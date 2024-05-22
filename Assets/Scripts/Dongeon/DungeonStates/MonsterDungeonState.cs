using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/MonsterDungeonState")]
    public class MonsterDungeonState : DungeonState
    {
        private Monster currentMonster = null;
        private PlayerCard playerCard = null;

        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();

            if(currentRoom.GetRoomType() == RoomType.Boss)
            {
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "BOSS !";
            }
            else
            {
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "MONSTER !";
            }

            if (currentMonster && currentMonster.GetCurrentHP() < 0)
            {
                DungeonUIManager.instance.CleanDescriptionCard();
                DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
            }
            else
            {
                Instantiate(CardsManager.instance.cardContinuePrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Continue:
                    {
                        if (!currentMonster)
                        {
                            DungeonUIManager.instance.CleanDescriptionCard();
                            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
                            currentMonster = Instantiate(CardsManager.instance.cardMonsterPrefab, DungeonUIManager.instance.GetMonsterPlacementPanel()).GetComponent<Monster>();
                            currentMonster.Setup(currentRoom.GetMonsterInfoAssociated());
                            currentMonster.GetComponent<Card>().ShowDownFace();
                            currentMonster.GetComponent<Card>().ReturnCard();

                            playerCard = Instantiate(CardsManager.instance.cardPlayerPrefab, DungeonUIManager.instance.GetPlayerPlacementPanel()).GetComponent<PlayerCard>();
                            playerCard.Setup(PlayerManager.instance.GetCurrentHP(), PlayerManager.instance.GetPlayerSprite());
                        }

                        if (currentMonster.GetCurrentHP() > 0)
                        {
                            DungeonUIManager.instance.CleanGameUI();

                            Instantiate(CardsManager.instance.cardAttackPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                            Instantiate(CardsManager.instance.cardUseObjectPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                            Instantiate(CardsManager.instance.cardRunAwayPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                        }
                    }
                    break;
                case CardType.Attack:
                    {
                        CombatAnimationManager.instance.Attack(playerCard, currentMonster);

                        return null;
                        /*playerCard.Attack();
                        currentMonster.TakeDamage(PlayerManager.instance.GetDamage());
                        if(currentMonster.GetCurrentHP() <= 0)
                        {
                            DungeonUIManager.instance.CleanDescriptionCard();
                            DungeonUIManager.instance.CleanMonsterCard();
                            DungeonUIManager.instance.CleanPlayerCard();

                            DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom().SetRoomType(RoomType.Empty);

                            return DungeonStatesManager.instance.chestDungeonStateInstance;
                        }

                        //monster reply
                        PlayerManager.instance.TakeDamage(currentMonster.GetDamage());
                        playerCard.SetHP(PlayerManager.instance.GetCurrentHP());*/
                    }
            }

            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
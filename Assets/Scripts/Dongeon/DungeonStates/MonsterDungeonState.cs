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

        private bool waitForEndOfCombat = false;

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

                            if (currentRoom.GetRoomType() == RoomType.Boss)
                            {
                                currentMonster.Setup(DungeonGeneratorManager.instance.GetDungeon().GetRoomInfo().bossInfo);
                            }
                            else
                            {
                                currentMonster.Setup(currentRoom.GetMonsterInfoAssociated());
                            }
                            
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
                        DungeonUIManager.instance.CleanGameUI();
                        CombatAnimationManager.instance.Attack(playerCard, currentMonster);
                        waitForEndOfCombat = true;
                        return null;
                    }
                case CardType.RunAwayRoom:
                    {
                        int rand = Random.Range(0, 100);
                        if(rand <= PlayerManager.instance.GetCurrentChance())
                        {
                            DungeonUIManager.instance.CleanDescriptionCard();
                            DungeonUIManager.instance.CleanMonsterCard();
                            DungeonUIManager.instance.CleanPlayerCard();
                            DungeonUIManager.instance.CleanScoreCard();

                            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You successfully escape from the monster. \n\nWhich way to go to ?";

                            return DungeonStatesManager.instance.endRoomDungeonStateInstance;
                        }
                        else
                        {
                            PlayerManager.instance.TakeDamage(currentMonster.GetDamage() * 0.1f);

                            if (PlayerManager.instance.GetCurrentHP() <= 0)
                            {                                
                                return null;
                            }

                            DungeonUIManager.instance.CleanDescriptionCard();
                            DungeonUIManager.instance.CleanMonsterCard();
                            DungeonUIManager.instance.CleanPlayerCard();
                            DungeonUIManager.instance.CleanScoreCard();

                            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You escape from the monster but you are hurt during your run. \n\nWhich way to go to ?";

                            return DungeonStatesManager.instance.endRoomDungeonStateInstance;
                        }
                    }
            }

            return null;
        }

        public override void OnStateUpdate()
        {
            if(waitForEndOfCombat)
            {
                if(!CombatAnimationManager.instance.GetIsAttacking())
                {
                    waitForEndOfCombat = false;
                    if (currentMonster && currentMonster.GetCurrentHP() > 0)
                    {
                        Instantiate(CardsManager.instance.cardAttackPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                        Instantiate(CardsManager.instance.cardUseObjectPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                        Instantiate(CardsManager.instance.cardRunAwayPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                    }
                }
            }
        }

        public override void OnStateExit()
        {
            waitForEndOfCombat = false;
        }
    }
}
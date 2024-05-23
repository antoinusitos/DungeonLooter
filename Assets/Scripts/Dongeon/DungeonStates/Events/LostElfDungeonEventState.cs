using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/Events/LostElfDungeonEventState")]
    public class LostElfDungeonEventState : DungeonState
    {
        private Card cardDesc = null;

        private int step = 0;

        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "What seems to be an elf is approaching you.";

            Instantiate(CardsManager.instance.cardContinuePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            step = 0;
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Continue:
                    if(step == 0)
                    {
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "\"Oh hello ! I'm a bit lost, I was looking for someone in this dungeon, you might have seen him ?\"";

                        CleanGameUI();

                        Instantiate(CardsManager.instance.cardUseObjectPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                        Instantiate(CardsManager.instance.cardRunAwayPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                        step++;
                    }
                    else if (step == 1)
                    {
                        DungeonUIManager.instance.CleanDescriptionCard();

                        CleanGameUI();

                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Which way to go to ?";

                        DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom().SetRoomType(RoomType.Empty);
                        DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
                    }
                    break;
                case CardType.SealedLetter:
                    CleanGameUI();
                    PlayerManager.instance.GetPlayerInventory().DesactivateCards();
                    Destroy(DungeonUIManager.instance.GetStartingObjectCard().gameObject);

                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "\"Ok, I'm joining you !\"";

                    Instantiate(CardsManager.instance.cardContinuePrefab, DungeonUIManager.instance.GetCardPlacementPanel());
                    break;

                case CardType.UseObject:
                    PlayerManager.instance.GetPlayerInventory().ActivateCards();
                    break;
            }
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
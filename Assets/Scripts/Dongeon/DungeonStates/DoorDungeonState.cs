using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/DoorDungeonState")]
    public class DoorDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(null);

            DungeonUIManager.instance.CleanDescriptionCard();
            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You face a door.";

            if(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor().GetDoorDirection() == DoorDirection.MonoDirectionnal)
            {
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "\nYou see that the door is one way.";
            }

            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "\n\nWhat do you do ?";

            Instantiate(CardsManager.instance.cardObservePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardUseObjectPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardOpenDoorPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardRunAwayDoorPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.OpenDoor:
                    {
                        //Handle Event
                        DungeonGeneratorManager.instance.GetDungeon().SetPreviousDoor(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor());
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                        DungeonUIManager.instance.CleanDescriptionCard();
                        return DungeonStatesManager.instance.startRoomDungeonStateInstance;
                    }
                case CardType.RunAwayDoor:
                    {
                        DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(DungeonGeneratorManager.instance.GetDungeon().GetPreviousRoom());
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                        DungeonUIManager.instance.CleanDescriptionCard();
                        return DungeonStatesManager.instance.startRoomDungeonStateInstance;
                    }
            }
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
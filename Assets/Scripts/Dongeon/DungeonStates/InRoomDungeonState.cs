using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/InRoomDungeonState")]
    public class InRoomDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();

            switch (currentRoom.GetRoomType())
            {
                case RoomType.Entry:
                    {
                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "It's the entry of the dungeon.\n\nWhich way to go to ?";
                        DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
                    }
                    break;
                case RoomType.Empty:
                    {
                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find an empty room.\n\nWhich way to go to ?";
                        DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
                    }
                    break;
                case RoomType.Monster:
                    DungeonUIManager.instance.CleanDescriptionCard();
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.monsterDungeonStateInstance);
                    break;
                case RoomType.Event:
                    DungeonUIManager.instance.CleanDescriptionCard();
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.eventDungeonStateInstance);
                    break;
                case RoomType.Chest:
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.chestDungeonStateInstance);
                    break;
                case RoomType.Boss:
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.monsterDungeonStateInstance);
                    break;
            }

            //Handle the event
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                default:
                    return null;
            }
        }

        public override void OnStateExit()
        {
        }
    }
}
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

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            switch (currentRoom.GetRoomType())
            {
                case RoomType.Entry:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "It's the entry of the dungeon.\n\nWhat do you do ?";
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
                    break;
                case RoomType.Empty:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find an empty room.\n\nWhat do you do ?";
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
                    break;
                case RoomType.Monster:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You face a big monster.\n\nWhat do you do ?";
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.monsterDungeonStateInstance);
                    break;
                case RoomType.Event:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find a random event.\n\nWhat do you do ?";
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.eventDungeonStateInstance);
                    break;
                case RoomType.Chest:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find an empty room with a chest inside.\n\nWhat do you do ?";
                    DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.chestDungeonStateInstance);
                    break;
                case RoomType.Boss:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Boss Room";
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
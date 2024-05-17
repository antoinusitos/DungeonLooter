using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/StartRoomDungeonState")]
    public class StartRoomDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonGeneratorManager.instance.GetDungeon().SetTargetRoom(DungeonGeneratorManager.instance.GetDungeon().GetNextRoom());

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You arrive at the entry of a room.\n\nWhat do you do ?";

            Instantiate(CardsManager.instance.cardObservePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardEnterPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardRunAwayPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.EnterRoom:
                    {
                        Room targetRoom = DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom();
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetRoom(null);
                        DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(targetRoom);

                        return DungeonStatesManager.instance.inRoomDungeonStateInstance;
                    }
                case CardType.RunAwayRoom:
                    {
                        if (DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom().GetRoomType() == RoomType.Entry)
                        {
                            //LEAVE DUNGEON
                            return null;
                        }

                        if (DungeonGeneratorManager.instance.GetDungeon().GetPreviousDoor().GetDoorDirection() == DoorDirection.MonoDirectionnal)
                        {
                            //CANNOT RETURN
                            return DungeonStatesManager.instance.turnAroundDungeonStateInstance;
                        }

                        DungeonGeneratorManager.instance.GetDungeon().SetPreviousRoom(DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom());
                        DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(DungeonGeneratorManager.instance.GetDungeon().GetPreviousDoor().GetOtherRoom(DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom()));
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetRoom(null);
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(DungeonGeneratorManager.instance.GetDungeon().GetPreviousDoor());
                        DungeonUIManager.instance.CleanDescriptionCard();
                        return DungeonStatesManager.instance.doorDungeonStateInstance;
                    }
                case CardType.Observe:
                    {
                        Debug.Log("Observe");
                        Room targetRoom = DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom();
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetRoom(null);
                        DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(targetRoom);
                        return DungeonStatesManager.instance.inRoomDungeonStateInstance;
                    }
                default:
                    return null;
            }
        }

        public override void OnStateExit()
        {
        }
    }
}
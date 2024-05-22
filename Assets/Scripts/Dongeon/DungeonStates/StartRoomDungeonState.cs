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
                        DungeonUIManager.instance.CleanDescriptionCard();
                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        float rand = Random.Range(0f, 100f);
                        if(rand <= PlayerManager.instance.GetCurrentPerception())
                        {
                            switch(DungeonGeneratorManager.instance.GetDungeon().GetTargetRoom().GetRoomType())
                            {
                                case RoomType.Monster:
                                    if(PlayerManager.instance.GetCurrentPerception() >= PlayerManager.instance.GetPerfectPerception())
                                    {
                                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "A monster is waiting in the room.\n\nWhat do you do ?";
                                    }
                                    else
                                    {
                                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "A shadow is standing in the room but you cannot distinguish it.\n\nWhat do you do ?";
                                    }
                                    break;
                                case RoomType.Event:
                                    if (PlayerManager.instance.GetCurrentPerception() >= PlayerManager.instance.GetPerfectPerception())
                                    {
                                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Someone is standing in the room, you are still not detected.\n\nWhat do you do ?";
                                    }
                                    else
                                    {
                                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "A shadow is standing in the room but you cannot distinguish it.\n\nWhat do you do ?";
                                    }
                                    break;
                                case RoomType.Chest:
                                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "It's an empty room with a chest inside.\n\nWhat do you do ?";
                                    break;
                                default:
                                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You see that the room is empty.\n\nWhat do you do ?";
                                    break;
                            }
                        }
                        else
                        {
                            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You try to see in the room and it seems empty.\n\nWhat do you do ?";
                        }
                        return null;
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
using AG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/EndRoomDungeonState")]
    public class EndRoomDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();

            if (currentRoom.GetLeftDoor())
            {
                Instantiate(CardsManager.instance.cardLeftPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetTopDoor())
            {
                Instantiate(CardsManager.instance.cardTopPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetRightDoor())
            {
                Instantiate(CardsManager.instance.cardRightPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetBottomDoor())
            {
                Instantiate(CardsManager.instance.cardBottomPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.GoLeft:
                    {
                        Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
                        if (currentRoom.GetLeftDoor())
                        {
                            Room otherRoom = currentRoom.GetLeftDoor().GetOtherRoom(currentRoom);
                            if (otherRoom)
                            {
                                DungeonGeneratorManager.instance.GetDungeon().SetPreviousRoom(currentRoom);
                                DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(currentRoom.GetLeftDoor());
                                DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(otherRoom);
                                DungeonUIManager.instance.CleanDescriptionCard();
                                return DungeonStatesManager.instance.doorDungeonStateInstance;
                            }
                        }
                    }
                    break;
                case CardType.GoRight:
                    {
                        Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
                        if (currentRoom.GetRightDoor())
                        {
                            Room otherRoom = currentRoom.GetRightDoor().GetOtherRoom(currentRoom);
                            if (otherRoom)
                            {
                                DungeonGeneratorManager.instance.GetDungeon().SetPreviousRoom(currentRoom);
                                DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(currentRoom.GetRightDoor());
                                DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(otherRoom);
                                DungeonUIManager.instance.CleanDescriptionCard();
                                return DungeonStatesManager.instance.doorDungeonStateInstance;
                            }
                        }
                    }
                    break;
                case CardType.GoTop:
                    {
                        Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
                        if (currentRoom.GetTopDoor())
                        {
                            Room otherRoom = currentRoom.GetTopDoor().GetOtherRoom(currentRoom);
                            if (otherRoom)
                            {
                                DungeonGeneratorManager.instance.GetDungeon().SetPreviousRoom(currentRoom);
                                DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(currentRoom.GetTopDoor());
                                DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(otherRoom);
                                DungeonUIManager.instance.CleanDescriptionCard();
                                return DungeonStatesManager.instance.doorDungeonStateInstance;
                            }
                        }
                    }
                    break;
                case CardType.GoBottom:
                    {
                        Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
                        if (currentRoom.GetBottomDoor())
                        {
                            Room otherRoom = currentRoom.GetBottomDoor().GetOtherRoom(currentRoom);
                            if (otherRoom)
                            {
                                DungeonGeneratorManager.instance.GetDungeon().SetPreviousRoom(currentRoom);
                                DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(currentRoom.GetBottomDoor());
                                DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(otherRoom);
                                DungeonUIManager.instance.CleanDescriptionCard();
                                return DungeonStatesManager.instance.doorDungeonStateInstance;
                            }
                        }
                    }
                    break;
            }
            return null;
        }

        public override void OnStateExit()
        {
            DungeonUIManager.instance.CleanDescriptionCard();
        }
    }
}
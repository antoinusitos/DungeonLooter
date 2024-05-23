using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonGeneratorManager : MonoBehaviour
    {
        public static DungeonGeneratorManager instance = null;

        [SerializeField]
        private Dungeon dungeonPrefab = null;

        private Dungeon instantiatedDungeon = null;

        [SerializeField]
        private DungeonFlow dungeonFlowPrefab = null;

        private DungeonFlow dungeonFlow = null;

        private float randomOneWayDoor = 0.5f;

        [SerializeField]
        private int minimumRoom = 5;
        [SerializeField]
        private int maximumRoom = 12;

        private int currentRoomNumber = 0;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void DestroyDungeon()
        {
            Destroy(instantiatedDungeon.gameObject);
            Destroy(dungeonFlow.gameObject);
            currentRoomNumber = 0;
        }

        public void GenerateDungeon()
        {
            //DEBUG
            //GenerateDummyDungeon();
            //return;

            instantiatedDungeon = Instantiate(dungeonPrefab);
            instantiatedDungeon.Setup();

            Room startingRoom = instantiatedDungeon.GetStartingRoom();
            startingRoom.SetRoomType(RoomType.Entry);
            instantiatedDungeon.SetTargetRoom(startingRoom);
            instantiatedDungeon.SetNextRoom(startingRoom);
            currentRoomNumber++;

            GenerateSurroundingRooms(startingRoom);

            if(currentRoomNumber < minimumRoom)
            {
                Debug.Log("Not Enough Rooms");
                GenerateSurroundingRooms(startingRoom);
            }

            instantiatedDungeon.Generate();

            instantiatedDungeon.Analyze();

            dungeonFlow = Instantiate(dungeonFlowPrefab);
            dungeonFlow.StartFlow();
        }

        public void GenerateSurroundingRooms(Room room)
        {
            if(currentRoomNumber >= maximumRoom)
            {
                return;
            }

            for (int directionIndex = 0; directionIndex < 4; directionIndex++)
            {
                float rand = Random.Range(0.0f, 1.0f);
                if (rand >= 0.5f)
                {
                    //LEFT
                    if (directionIndex == 0 && room.GetX() - 1 >= 0)
                    {
                        Room tempRoom = instantiatedDungeon.GetRoom(room.GetX() - 1, room.GetY());
                        if(tempRoom.GetIsUsed())
                        {
                            bool found = false;
                            for(int doorIndex = 0; doorIndex < room.GetDoors().Length; doorIndex++)
                            {
                                if (room.GetDoors()[doorIndex].GetOtherRoom(room) == tempRoom)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if(!found)
                            {
                                Door doorOneWay = instantiatedDungeon.SpawnDoor();
                                doorOneWay.LinkRooms(room, tempRoom);
                                tempRoom.AddDoor(doorOneWay);
                                if(Random.Range(0f, 1f) > randomOneWayDoor)
                                {
                                    room.AddDoor(doorOneWay);
                                }
                            }
                            continue;
                        }
                        tempRoom.SetIsUsed();
                        currentRoomNumber++;
                        Door door = instantiatedDungeon.SpawnDoor();
                        door.LinkRooms(room, tempRoom);
                        tempRoom.AddDoor(door);
                        room.AddDoor(door);
                        GenerateSurroundingRooms(tempRoom);
                    }
                    //TOP
                    else if (directionIndex == 1 && room.GetY() + 1 < instantiatedDungeon.GetDungeonRows())
                    {
                        Room tempRoom = instantiatedDungeon.GetRoom(room.GetX(), room.GetY() + 1);
                        if (tempRoom.GetIsUsed())
                        {
                            bool found = false;
                            for (int doorIndex = 0; doorIndex < room.GetDoors().Length; doorIndex++)
                            {
                                if (room.GetDoors()[doorIndex].GetOtherRoom(room) == tempRoom)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Door doorOneWay = instantiatedDungeon.SpawnDoor();
                                doorOneWay.LinkRooms(room, tempRoom);
                                tempRoom.AddDoor(doorOneWay);
                                if (Random.Range(0f, 1f) > randomOneWayDoor)
                                {
                                    room.AddDoor(doorOneWay);
                                }
                            }
                            continue;
                        }
                        tempRoom.SetIsUsed();
                        currentRoomNumber++;
                        Door door = instantiatedDungeon.SpawnDoor();
                        door.LinkRooms(room, tempRoom);
                        tempRoom.AddDoor(door);
                        room.AddDoor(door);
                        GenerateSurroundingRooms(tempRoom);
                    }
                    //RIGHT
                    else if (directionIndex == 2 && room.GetX() + 1 < instantiatedDungeon.GetDungeonRows())
                    {
                        Room tempRoom = instantiatedDungeon.GetRoom(room.GetX() + 1, room.GetY());
                        if (tempRoom.GetIsUsed())
                        {
                            bool found = false;
                            for (int doorIndex = 0; doorIndex < room.GetDoors().Length; doorIndex++)
                            {
                                if (room.GetDoors()[doorIndex].GetOtherRoom(room) == tempRoom)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Door doorOneWay = instantiatedDungeon.SpawnDoor();
                                doorOneWay.LinkRooms(room, tempRoom);
                                tempRoom.AddDoor(doorOneWay);
                                if (Random.Range(0f, 1f) > randomOneWayDoor)
                                {
                                    room.AddDoor(doorOneWay);
                                }
                            }
                            continue;
                        }
                        tempRoom.SetIsUsed();
                        currentRoomNumber++;
                        Door door = instantiatedDungeon.SpawnDoor();
                        door.LinkRooms(room, tempRoom);
                        tempRoom.AddDoor(door);
                        room.AddDoor(door);
                        GenerateSurroundingRooms(tempRoom);
                    }
                    //BOTTOM
                    else if (directionIndex == 3 && room.GetY() - 1 >= 0)
                    {
                        Room tempRoom = instantiatedDungeon.GetRoom(room.GetX(), room.GetY() - 1);
                        if (tempRoom.GetIsUsed())
                        {
                            bool found = false;
                            for (int doorIndex = 0; doorIndex < room.GetDoors().Length; doorIndex++)
                            {
                                if (room.GetDoors()[doorIndex].GetOtherRoom(room) == tempRoom)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Door doorOneWay = instantiatedDungeon.SpawnDoor();
                                doorOneWay.LinkRooms(room, tempRoom);
                                tempRoom.AddDoor(doorOneWay);
                                if (Random.Range(0f, 1f) > randomOneWayDoor)
                                {
                                    room.AddDoor(doorOneWay);
                                }
                            }
                            continue;
                        }
                        tempRoom.SetIsUsed();
                        currentRoomNumber++;
                        Door door = instantiatedDungeon.SpawnDoor();
                        door.LinkRooms(room, tempRoom);
                        tempRoom.AddDoor(door);
                        room.AddDoor(door);
                        GenerateSurroundingRooms(tempRoom);
                    }
                }
            }
        }

        public void GenerateDummyDungeon()
        {
            instantiatedDungeon = Instantiate(dungeonPrefab);
            instantiatedDungeon.Setup();

            Room startingRoom = instantiatedDungeon.GetStartingRoom();
            startingRoom.SetRoomType(RoomType.Entry);
            instantiatedDungeon.SetTargetRoom(startingRoom);
            instantiatedDungeon.SetNextRoom(startingRoom);

            Room room1 = instantiatedDungeon.GetRoom(startingRoom.GetX() - 1, startingRoom.GetY());
            room1.SetIsUsed();

            Room room2 = instantiatedDungeon.GetRoom(room1.GetX(), room1.GetY() + 1);
            room2.SetIsUsed();

            Room room3 = instantiatedDungeon.GetRoom(room1.GetX() - 1, room1.GetY());
            room3.SetIsUsed();

            Door door1 = instantiatedDungeon.SpawnDoor();
            door1.LinkRooms(startingRoom, room1);

            Door door2 = instantiatedDungeon.SpawnDoor();
            door2.LinkRooms(room1, room2);

            Door door3 = instantiatedDungeon.SpawnDoor();
            door3.LinkRooms(room1, room3);

            startingRoom.AddDoor(door1);

            room1.AddDoor(door2);
            room2.AddDoor(door2);

            room1.AddDoor(door3);
            room3.AddDoor(door3);

            instantiatedDungeon.Generate();

            instantiatedDungeon.Analyze();

            dungeonFlow = Instantiate(dungeonFlowPrefab);
            dungeonFlow.StartFlow();
        }

        public Dungeon GetDungeon()
        {
            return instantiatedDungeon;
        }

        public DungeonFlow GetDungeonFlow()
        {
            return dungeonFlow;
        }
    }
}
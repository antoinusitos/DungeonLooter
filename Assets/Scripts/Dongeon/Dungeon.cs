using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Dungeon : MonoBehaviour
    {
        [SerializeField]
        private int dungeonRows = 9;

        private float xOffset = 1;
        private float yOffset = 1;

        [SerializeField]
        private Room roomPrefab = null;

        [SerializeField]
        private Door doorPrefab = null;

        [SerializeField]
        private RoomInfo roomInfo = null;

        private Room[] generatedRooms = null;

        private List<Room> rooms = new List<Room>();
        private List<Door> doors = new List<Door>();

        private Room startingRoom = null;

        private Room currentRoom = null;

        private Room targetRoom = null;
        private Door targetDoor = null;

        private Room nextRoom = null;
        private Room previousRoom = null;

        private Door previousDoor = null;

        public Room GetStartingRoom()
        {
            return startingRoom;
        }

        public Room GetRoom(int x, int y)
        {
            return generatedRooms[y * dungeonRows + x];
        }

        public int GetDungeonRows()
        {
            return dungeonRows;
        }

        public void Setup()
        {
            generatedRooms = new Room[dungeonRows * dungeonRows];

            for (int y = 0; y < dungeonRows; y++)
            {
                for (int x = 0; x < dungeonRows; x++)
                {
                    generatedRooms[y * dungeonRows + x] = Instantiate(roomPrefab);
                    generatedRooms[y * dungeonRows + x].transform.SetParent(transform);
                    rooms.Add(generatedRooms[y * dungeonRows + x]);
                    generatedRooms[y * dungeonRows + x].SetXY(x, y);
                    generatedRooms[y * dungeonRows + x].transform.position = new Vector3(x * xOffset, y * yOffset, 0);

                    if (x == (dungeonRows - 1) / 2 && y == (dungeonRows - 1) / 2)
                    {
                        startingRoom = generatedRooms[y * dungeonRows + x];
                        startingRoom.SetIsUsed();
                    }
                }
            }
        }

        public void Generate()
        {
            //Use this to generate events
            for (int doorIndex = 0; doorIndex < doors.Count; doorIndex++)
            {
                doors[doorIndex].SetDoorType((DoorType)(Random.Range(1, (int)DoorType.MAX - 1)));
            }

            for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
            {
                if (rooms[roomIndex].GetRoomType() == RoomType.Entry)
                {
                    continue;
                }
                float random = Random.Range(0.0f, 100.0f);

                float cumulated = 0;
                for(int roomInfoIndex = 0; roomInfoIndex < roomInfo.roomTypeChances.Length; roomInfoIndex++)
                {
                    if(random <= cumulated + roomInfo.roomTypeChances[roomInfoIndex].chance)
                    {
                        rooms[roomIndex].SetRoomType(roomInfo.roomTypeChances[roomInfoIndex].roomType);
                        break;
                    }
                    cumulated += roomInfo.roomTypeChances[roomInfoIndex].chance;
                }

                switch(rooms[roomIndex].GetRoomType())
                {
                    case RoomType.Monster:
                        {
                            rooms[roomIndex].SetMonsterInfo(MonstersManager.instance.GetRandomMonster());
                        }
                        break;
                }
            }
        }

        public void Analyze()
        {
            for(int doorIndex = 0; doorIndex < doors.Count; doorIndex++)
            {
                bool room1HasDoor = doors[doorIndex].GetRoom1().HasDoor(doors[doorIndex]);
                bool room2HasDoor = doors[doorIndex].GetRoom2().HasDoor(doors[doorIndex]);

                if(room1HasDoor && room2HasDoor)
                {
                    doors[doorIndex].SetDoorDirection(DoorDirection.BiDirectionnal);
                }
                else
                {
                    doors[doorIndex].SetDoorDirection(DoorDirection.MonoDirectionnal);
                }

                doors[doorIndex].GetComponentInChildren<Renderer>().material.color = Color.black;
                doors[doorIndex].SetDoorType((DoorType)Random.Range(1, 3));
            }

            List<Room> potentialBossRoom = new List<Room>();

            for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
            {
                Door[] tempDoors = rooms[roomIndex].GetDoors();
                if(tempDoors.Length == 1)
                {
                    potentialBossRoom.Add(rooms[roomIndex]);
                }

                for (int doorIndex = 0; doorIndex < tempDoors.Length; doorIndex++)
                {
                    Room otherRoom = null;
                    if (tempDoors[doorIndex].GetRoom1() == rooms[roomIndex])
                    {
                        otherRoom = tempDoors[doorIndex].GetRoom2();
                    }
                    else
                    {
                        otherRoom = tempDoors[doorIndex].GetRoom1();
                    }

                    if (otherRoom.transform.position.x < rooms[roomIndex].transform.position.x)
                    {
                        rooms[roomIndex].SetLeftDoor(tempDoors[doorIndex]);
                    }
                    else if (otherRoom.transform.position.x > rooms[roomIndex].transform.position.x)
                    {
                        rooms[roomIndex].SetRightDoor(tempDoors[doorIndex]);
                    }
                    else if (otherRoom.transform.position.y > rooms[roomIndex].transform.position.y)
                    {
                        rooms[roomIndex].SetTopDoor(tempDoors[doorIndex]);
                    }
                    else
                    {
                        rooms[roomIndex].SetBottomDoor(tempDoors[doorIndex]);
                    }

                }
            }

            for (int roomIndex = 0; roomIndex < potentialBossRoom.Count; roomIndex++)
            {
                if (potentialBossRoom[roomIndex].GetRoomType() != RoomType.Entry)
                {
                    potentialBossRoom[roomIndex].SetRoomType(RoomType.Boss);
                    potentialBossRoom[roomIndex].GetComponentInChildren<Renderer>().material.color = Color.green;
                    Door[] doors = potentialBossRoom[roomIndex].GetDoors();
                    for (int doorIndex = 0; doorIndex < doors.Length; doorIndex++)
                    {
                        doors[doorIndex].SetDoorMaterial(DoorMaterial.Gold);
                    }
                    break;
                }
            }

            for (int y = 0; y < dungeonRows; y++)
            {
                for (int x = 0; x < dungeonRows; x++)
                {
                    generatedRooms[y * dungeonRows + x].GetComponentInChildren<Renderer>().material.color = Color.black;
                }
            }
        }

        public Door SpawnDoor()
        {
            Door door = Instantiate(doorPrefab);
            door.transform.SetParent(transform);
            doors.Add(door);
            return door;
        }

        public void SetCurrentRoom(Room inRoom)
        {
            if(currentRoom)
            {
                if (currentRoom.GetRoomType() == RoomType.Boss)
                {
                    currentRoom.GetComponentInChildren<Renderer>().material.color = Color.green;
                }
                else
                {
                    currentRoom.GetComponentInChildren<Renderer>().material.color = Color.white;
                }
            }
            currentRoom = inRoom;
            if (currentRoom)
            {
                currentRoom.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
        }

        public Room GetCurrentRoom()
        {
            return currentRoom;
        }

        public Room GetNextRoom()
        {
            return nextRoom;
        }

        public void SetNextRoom(Room room)
        {
            nextRoom = room;
        }

        public Room GetPreviousRoom()
        {
            return previousRoom;
        }

        public void SetPreviousRoom(Room room)
        {
            previousRoom = room;
        }

        public void SetTargetRoom(Room room)
        {
            if (targetRoom)
            {
                targetRoom.SetIsTargeted(false);
            }
            targetRoom = room;
            if (targetRoom)
            {
                targetRoom.SetIsTargeted(true);
            }
        }

        public Room GetTargetRoom()
        {
            return targetRoom;
        }

        public void SetTargetDoor(Door door)
        {
            if(targetDoor)
            {
                targetDoor.SetIsTargeted(false);
            }
            targetDoor = door;
            if (targetDoor)
            {
                targetDoor.SetIsTargeted(true);
            }
        }

        public Door GetTargetDoor()
        {
            return targetDoor;
        }

        public Door GetPreviousDoor()
        {
            return previousDoor;
        }

        public void SetPreviousDoor(Door door)
        {
            previousDoor = door;
        }

        public RoomInfo GetRoomInfo()
        {
            return roomInfo;
        }
    }
}
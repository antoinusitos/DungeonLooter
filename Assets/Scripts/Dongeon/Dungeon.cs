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

        private Room[] generatedRooms = null;

        private List<Room> rooms = new List<Room>();
        private List<Door> doors = new List<Door>();

        private Room startingRoom = null;

        private Room currentRoom = null;

        [Header("DEBUG")]
        [SerializeField]
        private bool moveLeft = false;
        [SerializeField]
        private bool moveRight = false;
        [SerializeField]
        private bool moveTop = false;
        [SerializeField]
        private bool moveBottom = false;

        [SerializeField]
        private bool canMoveLeft = false;
        [SerializeField]
        private bool canMoveRight = false;
        [SerializeField]
        private bool canMoveTop = false;
        [SerializeField]
        private bool canMoveBottom = false;

        public Room GetStartingRoom()
        {
            return startingRoom;
        }

        public Room GetRoom(int x, int y)
        {
            return generatedRooms[y * dungeonRows + x];
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
                        SetCurrentRoom(startingRoom);
                    }
                }
            }
        }

        public void Generate()
        {
            //Use this to generate events
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
            }

            for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
            {
                Door[] tempDoors = rooms[roomIndex].GetDoors();
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
        }

        public Door DebugSpawnDoor()
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
                currentRoom.GetComponentInChildren<Renderer>().material.color = Color.green;
            }
            currentRoom = inRoom;
            currentRoom.GetComponentInChildren<Renderer>().material.color = Color.red;
        }

        public Room GetCurrentRoom()
        {
            return currentRoom;
        }
    }
}
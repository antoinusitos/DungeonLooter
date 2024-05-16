using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Door : MonoBehaviour
    {
        private Room room1 = null;
        private Room room2 = null;

        private DoorDirection doorDirection = DoorDirection.None;

        public void LinkRooms(Room roomA, Room roomB)
        {
            room1 = roomA;
            room2 = roomB;

            transform.position = (room2.transform.position - room1.transform.position) / 2 + room1.transform.position;
        }

        public void SetDoorDirection(DoorDirection inDoorDirection)
        {
            doorDirection = inDoorDirection;
        }

        public Room GetRoom1()
        {
            return room1;
        }

        public Room GetRoom2()
        {
            return room2;
        }

        public Room GetOtherRoom(Room room)
        {
            if(room1 == room)
            {
                return room2;
            }
            else if (room2 == room)
            {
                return room1;   
            }
            return null;
        }
    }
}
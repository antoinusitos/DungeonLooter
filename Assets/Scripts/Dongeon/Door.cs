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

        private DoorType doorType = DoorType.None;

        private DoorMaterial doorMaterial = DoorMaterial.Wood;

        private bool isTargeted = false;

        private float targetValue = 0.0f;
        private float targetDirection = 1.0f;

        private void Update()
        {
            if(!isTargeted)
            {
                return;
            }

            GetComponentInChildren<Renderer>().material.color = new Color(targetValue, 0, 0);
            targetValue += Time.deltaTime * targetDirection;
            if(targetValue > 1)
            {
                targetDirection = -1;
            }
            else if(targetValue < 0)
            {
                targetDirection = 1;
            }
        }

        public void SetIsTargeted(bool state)
        {
            isTargeted = state;
            GetComponentInChildren<Renderer>().material.color = Color.white;
        }

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

        public DoorDirection GetDoorDirection()
        {
            return doorDirection;
        }

        public DoorMaterial GetDoorMaterial()
        {
            return doorMaterial;
        }

        public DoorType GetDoorType()
        {
            return doorType;
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

        public void SetDoorType(DoorType inDoorType)
        {
            doorType = inDoorType;
        }

        public void SetDoorMaterial(DoorMaterial inDoorMaterial)
        {
            doorMaterial = inDoorMaterial;
        }
    }
}
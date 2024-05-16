using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Room : MonoBehaviour
    {
        private bool isActive = false;

        private int x = 0;
        private int y = 0;

        private List<Door> doors = new List<Door>();

        private Door leftDoor = null;
        private Door rightDoor = null;
        private Door topDoor = null;
        private Door bottomDoor = null;

        private RoomType roomType = RoomType.None;

        public void SetXY(int inX,  int inY)
        {
            x = inX;
            y = inY;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY() 
        { 
            return y; 
        }

        public void SetIsUsed()
        {
            isActive = true;
            GetComponentInChildren<Renderer>().material.color = Color.green;
        }

        public void SetRoomType(RoomType inRoomType)
        {
            roomType = inRoomType;
        }

        public RoomType GetRoomType()
        {
            return roomType;
        }

        public void AddDoor(Door door)
        {
            doors.Add(door);
        }

        public bool HasDoor(Door door)
        {
            return doors.Contains(door);
        }

        public Door[] GetDoors()
        {
            return doors.ToArray();
        }

        public void SetTopDoor(Door door)
        {
            topDoor = door;
        }

        public void SetBottomDoor(Door door)
        {
            bottomDoor = door;
        }

        public void SetRightDoor(Door door)
        {
            rightDoor = door;
        }

        public void SetLeftDoor(Door door)
        {
            leftDoor = door;
        }

        public Door GetLeftDoor()
        {
            return leftDoor;
        }

        public Door GetRightDoor()
        {
            return rightDoor;
        }

        public Door GetTopDoor()
        {
            return topDoor;
        }

        public Door GetBottomDoor()
        {
            return bottomDoor;
        }
    }
}
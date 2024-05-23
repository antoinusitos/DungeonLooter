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

        private bool isTargeted = false;

        private float targetValue = 0.0f;
        private float targetDirection = 1.0f;

        private MonsterInfo monsterInfoAssociated = null;

        private bool isCompleted = false;

        private void Update()
        {
            if (!isTargeted)
            {
                return;
            }

            GetComponentInChildren<Renderer>().material.color = new Color(targetValue, 0, 0);
            targetValue += Time.deltaTime * targetDirection;
            if (targetValue > 1)
            {
                targetDirection = -1;
            }
            else if (targetValue < 0)
            {
                targetDirection = 1;
            }
        }

        public void SetIsTargeted(bool state)
        {
            isTargeted = state;
            GetComponentInChildren<Renderer>().material.color = Color.white;
        }
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
            //GetComponentInChildren<Renderer>().material.color = Color.white;
        }

        public bool GetIsUsed()
        {
            return isActive;
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

        public void SetMonsterInfo(MonsterInfo monsterInfo)
        {
            monsterInfoAssociated = monsterInfo;
        }

        public MonsterInfo GetMonsterInfoAssociated()
        {
            return monsterInfoAssociated;
        }

        public void SetIsCompleted()
        {
            isCompleted = true;
        }

        public bool GetIsCompleted()
        {
            return isCompleted;
        }
    }
}
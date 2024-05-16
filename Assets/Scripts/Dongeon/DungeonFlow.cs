using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class DungeonFlow : MonoBehaviour
    {
        [SerializeField]
        private Card cardPrefab = null;

        [Header("End Room")]
        [SerializeField]
        private Card cardLeftPrefab = null;
        [SerializeField]
        private Card cardRightPrefab = null;
        [SerializeField]
        private Card cardTopPrefab = null;
        [SerializeField]
        private Card cardBottomPrefab = null;

        [Header("Start Room")]
        [SerializeField]
        private Card cardObservePrefab = null;
        [SerializeField]
        private Card cardEnterPrefab = null;
        [SerializeField]
        private Card cardRunAwayPrefab = null;
        [SerializeField]
        private Card cardDescriptionPrefab = null;

        private DungeonStateType dungeonStateType = DungeonStateType.StartRoom;

        public void StartFlow()
        {
            AnalyzeSitutation();
        }

        public void AnalyzeSitutation()
        {
            switch(dungeonStateType)
            {
                case DungeonStateType.StartRoom:
                    HandleStartRoom();
                    break;
                case DungeonStateType.EndRoom:
                    HandleEndRoomFlow();
                    break;
            }
        }

        public void HandleStartRoom()
        {
            CleanGameUI();

            Card cardDesc = Instantiate(cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You arrive at the entry of a room.\n\nWhat do you do ?";

            Instantiate(cardObservePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(cardEnterPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(cardRunAwayPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public void HandleEndRoomFlow()
        {
            CleanGameUI();

            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();

            if(currentRoom.GetLeftDoor())
            {
                Instantiate(cardLeftPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetTopDoor())
            {
                Instantiate(cardTopPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetRightDoor())
            {
                Instantiate(cardRightPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
            if (currentRoom.GetBottomDoor())
            {
                Instantiate(cardBottomPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            }
        }

        private void CleanGameUI()
        {
            DungeonUIManager.instance.CleanGameUI();
        }

        #region Actions

        public void GoLeft()
        {
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
            if (currentRoom.GetLeftDoor())
            {
                Room otherRoom = currentRoom.GetLeftDoor().GetOtherRoom(currentRoom);
                if (otherRoom)
                {
                    DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(otherRoom);
                    dungeonStateType = DungeonStateType.StartRoom;
                    DungeonUIManager.instance.CleanDescriptionCard();
                    AnalyzeSitutation();
                }
            }
        }

        public void GoRight()
        {
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
            if (currentRoom.GetRightDoor())
            {
                Room otherRoom = currentRoom.GetRightDoor().GetOtherRoom(currentRoom);
                if (otherRoom)
                {
                    DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(otherRoom);
                    dungeonStateType = DungeonStateType.StartRoom;
                    DungeonUIManager.instance.CleanDescriptionCard();
                    AnalyzeSitutation();
                }
            }
        }

        public void GoTop()
        {
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
            if (currentRoom.GetTopDoor())
            {
                Room otherRoom = currentRoom.GetTopDoor().GetOtherRoom(currentRoom);
                if (otherRoom)
                {
                    DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(otherRoom);
                    dungeonStateType = DungeonStateType.StartRoom;
                    DungeonUIManager.instance.CleanDescriptionCard();
                    AnalyzeSitutation();
                }
            }
        }

        public void GoBottom()
        {
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
            if (currentRoom.GetBottomDoor())
            {
                Room otherRoom = currentRoom.GetBottomDoor().GetOtherRoom(currentRoom);
                if (otherRoom)
                {
                    DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(otherRoom);
                    dungeonStateType = DungeonStateType.StartRoom;
                    DungeonUIManager.instance.CleanDescriptionCard();
                    AnalyzeSitutation();
                }
            }
        }

        public void EnterRoom()
        {
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();
            //Handle the event

            DungeonUIManager.instance.CleanDescriptionCard();

            Card cardDesc = Instantiate(cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            switch (currentRoom.GetRoomType())
            {
                case RoomType.Entry:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "It's the entry of the dungeon.\n\nWhat do you do ?";
                    break;
                case RoomType.Empty:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find an empty room.\n\nWhat do you do ?";
                    break;
                case RoomType.Monster:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You face a big monster.\n\nWhat do you do ?";
                    break;
                case RoomType.Event:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find a random event.\n\nWhat do you do ?";
                    break;
                case RoomType.Chest:
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You find an empty room with a chest inside.\n\nWhat do you do ?";
                    break;
            }

            dungeonStateType = DungeonStateType.EndRoom;
            AnalyzeSitutation();
        }
        #endregion
    }
}
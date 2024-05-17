using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class StartScreen : MonoBehaviour
    {
        [Header("Race")]
        [SerializeField]
        private GameObject raceSelectionPrefab = null;
        [SerializeField]
        private Transform raceSelectionPanel = null;

        private int raceIndex = 0;
        private UI_RaceSelection raceSelectionInstance = null;

        [Header("Class")]
        [SerializeField]
        private GameObject classSelectionPrefab = null;
        [SerializeField]
        private Transform classSelectionPanel = null;

        private int classIndex = 0;
        private UI_ClassSelection classSelectionInstance = null;

        [Header("Item")]
        [SerializeField]
        private GameObject itemSelectionPrefab = null;
        [SerializeField]
        private Transform itemSelectionPanel = null;

        private int itemIndex = 0;
        private UI_ItemSelection itemSelectionInstance = null;

        private void Start()
        {
            SetupRace();
            SetupClass();
            SetupItem();
        }

        public void StartGame()
        {
            PlayerManager.instance.SetRaceInfo(InfoManager.instance.GetRaceInfoWithIndex(raceIndex));
            PlayerManager.instance.SetClassInfo(InfoManager.instance.GetClassInfoWithIndex(classIndex));
            PlayerManager.instance.SetStartingObject(InfoManager.instance.GetItemInfoWithIndex(itemIndex));

            DungeonGeneratorManager.instance.GenerateDungeon();
        }

        #region RACE

        private void SetupRace()
        {
            raceSelectionInstance = Instantiate(raceSelectionPrefab, raceSelectionPanel).GetComponent<UI_RaceSelection>();
            UpdateRace();
        }

        public void NextRace()
        {
            raceIndex++;
            if (raceIndex >= InfoManager.instance.GetRacesNumber())
            {
                raceIndex = 0;
            }

            UpdateRace();
        }

        public void PreviousRace()
        {
            raceIndex--;
            if (raceIndex < 0)
            {
                raceIndex = InfoManager.instance.GetRacesNumber() - 1;
            }

            UpdateRace();
        }
        private void UpdateRace()
        {
            raceSelectionInstance.SetRace(InfoManager.instance.GetRaceInfoWithIndex(raceIndex));
        }

        #endregion

        #region CLASS

        private void SetupClass()
        {
            classSelectionInstance = Instantiate(classSelectionPrefab, classSelectionPanel).GetComponent<UI_ClassSelection>();
            UpdateClass();
        }

        public void NextClass()
        {
            classIndex++;
            if (classIndex >= InfoManager.instance.GetClassesNumber())
            {
                classIndex = 0;
            }

            UpdateClass();
        }

        public void PreviousClass()
        {
            classIndex--;
            if (classIndex < 0)
            {
                classIndex = InfoManager.instance.GetClassesNumber() - 1;
            }

            UpdateClass();
        }

        private void UpdateClass()
        {
            classSelectionInstance.SetClass(InfoManager.instance.GetClassInfoWithIndex(classIndex));
        }
        #endregion

        #region ITEM

        private void SetupItem()
        {
            itemSelectionInstance = Instantiate(itemSelectionPrefab, itemSelectionPanel).GetComponent<UI_ItemSelection>();
            UpdateItem();
        }

        public void NextItem()
        {
            itemIndex++;
            if (itemIndex >= InfoManager.instance.GetItemsNumber())
            {
                itemIndex = 0;
            }

            UpdateItem();
        }

        public void PreviousItem()
        {
            itemIndex--;
            if (itemIndex < 0)
            {
                itemIndex = InfoManager.instance.GetItemsNumber() - 1;
            }

            UpdateItem();
        }

        private void UpdateItem()
        {
            itemSelectionInstance.SetItem(InfoManager.instance.GetItemInfoWithIndex(itemIndex));
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance = null;

        private Race startingRace = Race.None;
        private RaceInfo raceInfo = null;

        private PlayerClass startingClass = PlayerClass.None;
        private ClassInfo classInfo = null;

        private StartingObject startingObject = StartingObject.None;
        private ItemInfo itemInfo = null;

        private float currentHP = 0.0f;
        private float currentChance = 0.0f;
        private float currentPerception = 0.0f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetRaceInfo(RaceInfo inRaceInfo)
        {
            raceInfo = inRaceInfo;
            startingRace = raceInfo.race;

            for (int raceIndex = 0; raceIndex < raceInfo.raceStats.Length; raceIndex++)
            {
                if (raceInfo.raceStats[raceIndex].modifier == Modifier.HP)
                {
                    currentHP = raceInfo.raceStats[raceIndex].value;
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Chance)
                {
                    currentChance = raceInfo.raceStats[raceIndex].value;
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Perception)
                {
                    currentPerception = raceInfo.raceStats[raceIndex].value;
                }
            }

            DungeonUIManager.instance.GetRaceCard().SetRace(raceInfo);
        }

        public void SetClassInfo(ClassInfo inClassInfo)
        {
            classInfo = inClassInfo;
            startingClass = classInfo.playerClass;

            DungeonUIManager.instance.GetClassCard().SetClass(classInfo);
        }

        public void SetStartingObject(ItemInfo inItemInfo)
        {
            itemInfo = inItemInfo;
            startingObject = inItemInfo.itemClass;

            DungeonUIManager.instance.GetStartingObjectCard().SetObject(itemInfo);
        }

        public float GetCurrentHP()
        {
            return currentHP;
        }

        public float GetCurrentChance()
        {
            return currentChance;
        }

        public float GetCurrentPerception()
        {
            return currentPerception;
        }

        public Race GetRace()
        {
            return startingRace;
        }

        public PlayerClass GetClass()
        {
            return startingClass;
        }

        public StartingObject GetStartingObject()
        {
            return startingObject;
        }
    }
}
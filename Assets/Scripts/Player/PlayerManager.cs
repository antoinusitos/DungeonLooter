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
        private StartingItemInfo itemInfo = null;

        private float currentHP = 0.0f;
        private float maxHP = 0.0f;
        private float currentChance = 0.0f;
        private float currentPerception = 0.0f;

        private float currentDamage = 0.0f;

        private float currentCritical = 2;

        private float currentFailChance = 0;
        private float currentCriticChance = 0;

        private PlayerInventory playerInventory = null;

        private const float perfectPerception = 80;

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

        private void Start()
        {
            playerInventory = GetComponent<PlayerInventory>();
        }

        public PlayerInventory GetPlayerInventory()
        {
            return playerInventory;
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
                    maxHP = currentHP;
                    DungeonUIManager.instance.SetHPText(currentHP, maxHP);
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Chance)
                {
                    currentChance = raceInfo.raceStats[raceIndex].value;
                    DungeonUIManager.instance.SetChanceText(currentChance);
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Perception)
                {
                    currentPerception = raceInfo.raceStats[raceIndex].value;
                    DungeonUIManager.instance.SetPerceptionText(currentPerception);
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Damage)
                {
                    currentDamage = raceInfo.raceStats[raceIndex].value;
                    DungeonUIManager.instance.SetDamageText(currentDamage);
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.FailChance)
                {
                    currentFailChance = raceInfo.raceStats[raceIndex].value;
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.CriticChance)
                {
                    currentCriticChance = raceInfo.raceStats[raceIndex].value;
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

        public void SetStartingObject(StartingItemInfo inItemInfo)
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

        public float GetCurrentFailChance()
        {
            return currentFailChance;
        }

        public float GetCurrentCriticChance()
        {
            return currentCriticChance;
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

        public float GetDamage()
        {
            return currentDamage;
        }

        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            DungeonUIManager.instance.SetHPText(currentHP, maxHP);

            if(currentHP <= 0)
            {
                playerInventory.CleanInventory();
                DungeonUIManager.instance.ShowEndLoadingScreen();
            }
        }

        public void ModifyDamage(float inValue)
        {
            currentDamage += inValue;
            DungeonUIManager.instance.SetDamageText(currentDamage);
        }

        public void ModifyChance(float inValue)
        {
            currentChance += inValue;
            DungeonUIManager.instance.SetChanceText(currentChance);
        }

        public void ModifyPerception(float inValue)
        {
            currentPerception += inValue;
            DungeonUIManager.instance.SetPerceptionText(currentPerception);
        }

        public void ModifyHPMax(float inValue)
        {
            maxHP += inValue;
            DungeonUIManager.instance.SetHPText(currentHP, maxHP);
        }

        public float GetPerfectPerception()
        {
            return perfectPerception;
        }

        public Sprite GetPlayerSprite()
        {
            return raceInfo.raceImage;
        }

        public void RefillHP(float value)
        {
            currentHP += value;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
            DungeonUIManager.instance.SetHPText(currentHP, maxHP);
            if(DungeonUIManager.instance.GetPlayerPlacementPanel().childCount > 0)
            {
                Transform playerCard = DungeonUIManager.instance.GetPlayerPlacementPanel().GetChild(0);
                if (playerCard)
                {
                    playerCard.GetComponent<PlayerCard>().SetHP(currentHP);
                }
            }
        }

        public float GetCurrentCritical()
        {
            return currentCritical;
        }

        public void SetCurrentCritical(float value)
        {
            currentCritical = value;
        }
    }
}
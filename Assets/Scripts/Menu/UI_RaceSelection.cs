using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class UI_RaceSelection : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI raceName = null;

        [SerializeField]
        private TextMeshProUGUI raceDesc = null;

        [SerializeField]
        private TextMeshProUGUI raceHP = null;

        [SerializeField]
        private TextMeshProUGUI raceDamage = null;

        [SerializeField]
        private TextMeshProUGUI raceChance = null;

        [SerializeField]
        private Image raceImage = null;

        public void SetRace(RaceInfo raceInfo)
        {
            raceName.text = raceInfo.raceName;
            raceDesc.text = raceInfo.raceDescription;
            raceImage.sprite = raceInfo.raceImage;
            for(int statIndex = 0; statIndex < raceInfo.raceStats.Length;  statIndex++)
            {
                switch(raceInfo.raceStats[statIndex].modifier)
                {
                    case Modifier.HP:
                        raceHP.text = "HP: " + raceInfo.raceStats[statIndex].value;
                        break;
                    case Modifier.Damage:
                        raceDamage.text = "Damage: " + raceInfo.raceStats[statIndex].value;
                        break;
                    case Modifier.Chance:
                        raceChance.text = "Chance: " + raceInfo.raceStats[statIndex].value;
                        break;
                }
            }
        }
    }
}
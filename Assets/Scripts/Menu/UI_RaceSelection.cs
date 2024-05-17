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
        private TextMeshProUGUI raceStats = null;

        [SerializeField]
        private Image raceImage = null;

        public void SetRace(RaceInfo raceInfo)
        {
            raceName.text = raceInfo.raceName;
            raceDesc.text = raceInfo.raceDescription;
            raceImage.sprite = raceInfo.raceImage;
            raceStats.text = "";
            for (int statIndex = 0; statIndex < raceInfo.raceStats.Length;  statIndex++)
            {
                switch(raceInfo.raceStats[statIndex].modifier)
                {
                    case Modifier.HP:
                        raceStats.text += "HP: " + raceInfo.raceStats[statIndex].value + "\n";
                        break;
                    case Modifier.Damage:
                        raceStats.text += "Damage: " + raceInfo.raceStats[statIndex].value + "\n";
                        break;
                    case Modifier.Chance:
                        raceStats.text += "Chance: " + raceInfo.raceStats[statIndex].value + "\n";
                        break;
                    case Modifier.Perception:
                        raceStats.text += "Perception: " + raceInfo.raceStats[statIndex].value + "\n";
                        break;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class RaceCard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI raceName = null;

        [SerializeField]
        private TextMeshProUGUI raceDesc = null;

        [SerializeField]
        private TextMeshProUGUI raceStats = null;

        public void SetRace(RaceInfo raceInfo)
        {
            raceName.text = raceInfo.raceName;
            raceDesc.text = raceInfo.raceDescription;

            raceStats.text = "";
            for (int raceIndex = 0; raceIndex < raceInfo.raceStats.Length; raceIndex++)
            {
                if (raceInfo.raceStats[raceIndex].modifier == Modifier.HP)
                {
                    raceStats.text += "HP : " + PlayerManager.instance.GetCurrentHP() + "\n";
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Chance)
                {
                    raceStats.text += "Chance : " + PlayerManager.instance.GetCurrentChance() + "\n";
                }
                else if (raceInfo.raceStats[raceIndex].modifier == Modifier.Perception)
                {
                    raceStats.text += "Perception : " + PlayerManager.instance.GetCurrentPerception() + "\n";
                }
            }
        }
    }
}
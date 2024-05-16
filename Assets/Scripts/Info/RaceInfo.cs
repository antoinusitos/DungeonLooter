using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Creation/Infos/Race")]
    public class RaceInfo : ScriptableObject
    {
        public Race race = Race.None;

        public string raceName = "";
        public string raceDescription = "";

        public Sprite raceImage = null;

        public Stat[] raceStats = null;
    }
}
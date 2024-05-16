using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class PlayerManager : MonoBehaviour
    {
        private Race startingRace = Race.None;
        private RaceInfo raceInfo = null;

        private PlayerClass startingClass = PlayerClass.None;
        private ClassInfo classInfo = null;

        private StartingObject startingObject = StartingObject.None;
    }
}
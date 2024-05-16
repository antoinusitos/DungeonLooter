using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Saving/Race")]
    public class RaceAvailability : ScriptableObject
    {
        public Race race = Race.None;
        public bool available = false;
        public bool availableAtStart = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonEventsManager : MonoBehaviour
    {
        public static DungeonEventsManager instance = null;

        [SerializeField]
        private DungeonState[] events = null;

        public DungeonState[] eventsInstance = null;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            eventsInstance = new DungeonState[events.Length];
            for (int eventIndex = 0; eventIndex < events.Length; eventIndex++)
            {
                eventsInstance[eventIndex] = Instantiate(events[eventIndex]);
            }
        }
    }
}
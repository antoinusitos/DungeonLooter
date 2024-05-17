using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonStatesManager : MonoBehaviour
    {
        public static DungeonStatesManager instance = null;

        [SerializeField]
        private StartRoomDungeonState startRoomDungeonState = null;
        [SerializeField]
        private EndRoomDungeonState endRoomDungeonState = null;
        [SerializeField]
        private DoorDungeonState doorDungeonState = null;
        [SerializeField]
        private TurnAroundDungeonState turnAroundDungeonState = null;
        [SerializeField]
        private InRoomDungeonState inRoomDungeonState = null;
        [SerializeField]
        private EventDungeonState eventDungeonState = null;
        [SerializeField]
        private MonsterDungeonState monsterDungeonState = null;
        [SerializeField]
        private ChestDungeonState chestDungeonState = null;

        [HideInInspector]
        public StartRoomDungeonState startRoomDungeonStateInstance = null;
        [HideInInspector]
        public EndRoomDungeonState endRoomDungeonStateInstance = null;
        [HideInInspector]
        public DoorDungeonState doorDungeonStateInstance = null;
        [HideInInspector]
        public TurnAroundDungeonState turnAroundDungeonStateInstance = null;
        [HideInInspector]
        public InRoomDungeonState inRoomDungeonStateInstance = null;
        [HideInInspector]
        public EventDungeonState eventDungeonStateInstance = null;
        [HideInInspector]
        public MonsterDungeonState monsterDungeonStateInstance = null;
        [HideInInspector]
        public ChestDungeonState chestDungeonStateInstance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            startRoomDungeonStateInstance = Instantiate(startRoomDungeonState);
            endRoomDungeonStateInstance = Instantiate(endRoomDungeonState);
            doorDungeonStateInstance = Instantiate(doorDungeonState);
            turnAroundDungeonStateInstance = Instantiate(turnAroundDungeonState);
            inRoomDungeonStateInstance = Instantiate(inRoomDungeonState);
            eventDungeonStateInstance = Instantiate(eventDungeonState);
            monsterDungeonStateInstance = Instantiate(monsterDungeonState);
            chestDungeonStateInstance = Instantiate(chestDungeonState);
        }
    }
}
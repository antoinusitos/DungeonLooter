using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonGeneratorManager : MonoBehaviour
    {
        public static DungeonGeneratorManager instance = null;

        [SerializeField]
        private Dungeon dungeonPrefab = null;

        private Dungeon instantiatedDungeon = null;

        [SerializeField]
        private DungeonFlow dungeonFlowPrefab = null;

        private DungeonFlow dungeonFlow = null;

        private void Awake()
        {
            if(instance == null)
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
        }

        public void GenerateDummyDungeon()
        {
            instantiatedDungeon = Instantiate(dungeonPrefab);
            instantiatedDungeon.Setup();

            Room startingRoom = instantiatedDungeon.GetStartingRoom();
            startingRoom.SetRoomType(RoomType.Entry);
            Room room1 = instantiatedDungeon.GetRoom(startingRoom.GetX() - 1, startingRoom.GetY());
            room1.SetIsUsed();
            room1.SetRoomType((RoomType)(Random.Range(2, (int)RoomType.MAX - 1)));

            Room room2 = instantiatedDungeon.GetRoom(room1.GetX(), room1.GetY() + 1);
            room2.SetIsUsed();
            room2.SetRoomType((RoomType)(Random.Range(2, (int)RoomType.MAX - 1)));

            Room room3 = instantiatedDungeon.GetRoom(room1.GetX() - 1, room1.GetY());
            room3.SetIsUsed();
            room3.SetRoomType((RoomType)(Random.Range(2, (int)RoomType.MAX - 1)));

            Door door1 = instantiatedDungeon.DebugSpawnDoor();
            door1.LinkRooms(startingRoom, room1);

            Door door2 = instantiatedDungeon.DebugSpawnDoor();
            door2.LinkRooms(room1, room2);

            Door door3 = instantiatedDungeon.DebugSpawnDoor();
            door3.LinkRooms(room1, room3);

            startingRoom.AddDoor(door1);

            room1.AddDoor(door2);
            room2.AddDoor(door2);

            room1.AddDoor(door3);
            room3.AddDoor(door3);

            instantiatedDungeon.Generate();

            instantiatedDungeon.Analyze();

            dungeonFlow = Instantiate(dungeonFlowPrefab);
            dungeonFlow.StartFlow();
        }

        public Dungeon GetDungeon()
        {
            return instantiatedDungeon;
        }

        public DungeonFlow GetDungeonFlow()
        {
            return dungeonFlow;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [System.Serializable]
    public class RoomTypeChance
    {
        public RoomType roomType = RoomType.None;
        public float chance = 0;
    }

    [CreateAssetMenu(menuName = "Dungeon/Infos/Room")]
    public class RoomInfo : ScriptableObject
    {
        public RoomTypeChance[] roomTypeChances = null;

        public MonsterInfo bossInfo = null;
    }
}
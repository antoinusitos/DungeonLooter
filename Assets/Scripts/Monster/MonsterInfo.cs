using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "AI/Monster")]
    public class MonsterInfo : ScriptableObject
    {
        public int ID = -1;

        public string monsterName = "";
        public string monsterDescription = "";

        public Sprite monsterImage = null;

        public Stat[] monsterStats = null;
    }
}
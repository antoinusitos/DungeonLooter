using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class MonstersManager : MonoBehaviour
    {
        public static MonstersManager instance = null;

        [SerializeField]
        private MonsterInfo[] monsterInfos = null;

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

            for (int monsterIndex = 0; monsterIndex < monsterInfos.Length; monsterIndex++)
            {
                monsterInfos[monsterIndex].ID = monsterIndex;
            }
        }

        public MonsterInfo GetMonsterInfoWithID(int id)
        {
            for(int monsterIndex = 0;  monsterIndex < monsterInfos.Length; monsterIndex++)
            {
                if (monsterInfos[monsterIndex].ID == id)
                {
                    return monsterInfos[monsterIndex];
                }
            }
            return null;
        }

        public MonsterInfo GetRandomMonster()
        {
            return monsterInfos[Random.Range(0, monsterInfos.Length)];
        }
    }
}
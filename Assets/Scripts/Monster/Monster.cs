using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class Monster : MonoBehaviour
    {
        private MonsterInfo monsterInfo = null;

        [SerializeField]
        private RawImage monsterSprite = null;
        [SerializeField]
        private TextMeshProUGUI monsterNameText = null;
        [SerializeField]
        private TextMeshProUGUI monsterHPText = null;

        private float currentHP = 0;

        public void Setup(MonsterInfo inMonsterInfo)
        {
            monsterInfo = inMonsterInfo;
            monsterNameText.text = monsterInfo.monsterName;
            monsterSprite.texture = monsterInfo.monsterImage.texture;
            for (int statIndex = 0; statIndex < monsterInfo.monsterStats.Length;  statIndex++)
            {
                if (monsterInfo.monsterStats[statIndex].modifier == Modifier.HP)
                {
                    currentHP = monsterInfo.monsterStats[statIndex].value;
                    monsterHPText.text = currentHP.ToString();
                }
            }
        }

        public float GetCurrentHP()
        {
            return currentHP;
        }

        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            monsterHPText.text = currentHP.ToString();
        }

        public float GetDamage()
        {
            for (int statIndex = 0; statIndex < monsterInfo.monsterStats.Length; statIndex++)
            {
                if (monsterInfo.monsterStats[statIndex].modifier == Modifier.Damage)
                {
                    return monsterInfo.monsterStats[statIndex].value;
                }
            }
            return 0;
        }
    }
}
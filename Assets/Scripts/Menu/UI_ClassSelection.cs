using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class UI_ClassSelection : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI className = null;

        [SerializeField]
        private TextMeshProUGUI classDesc = null;

        [SerializeField]
        private TextMeshProUGUI classStat = null;

        [SerializeField]
        private Image classImage = null;

        public void SetClass(ClassInfo classInfo)
        {
            className.text = classInfo.className;
            classDesc.text = classInfo.classDescription;
            classImage.sprite = classInfo.classImage;
            classStat.text = "";

            if(classInfo.classStats.Length == 0)
            {
                classStat.text = classInfo.classStatDescription;
                return;
            }

            for (int statIndex = 0; statIndex < classInfo.classStats.Length;  statIndex++)
            {
                string statDesc = classInfo.classStatDescription.Replace("%%", classInfo.classStats[statIndex].value.ToString());

                classStat.text = statDesc;

                /*switch (classInfo.classStats[statIndex].modifier)
                {
                    case Modifier.AllyStats:
                        classStat.text = "x" + classInfo.classStats[statIndex].value + " to ally stats";
                        break;
                    case Modifier.Critical:
                        classStat.text = "Critical deals x" + classInfo.classStats[statIndex].value + " damage instead of x2";
                        break;
                    case Modifier.DamageAgainstDead:
                        classStat.text = "Damage x" + classInfo.classStats[statIndex].value + " against dead opponents";
                        break;
                    case Modifier.DamageAgainstFlying:
                        classStat.text = "Damage x" + classInfo.classStats[statIndex].value + " against flying opponents";
                        break;
                    case Modifier.DamageAgainstLiving:
                        classStat.text = "Damage x" + classInfo.classStats[statIndex].value + " against living opponents";
                        break;
                    case Modifier.Detection:
                        classStat.text = "Detection of hidden objects and locations";
                        break;
                    case Modifier.ImmuneToFemale:
                        classStat.text = "Immune against female opponents";
                        break;
                    case Modifier.MoreInfo:
                        classStat.text = "More general infos about enemies and environment";
                        break;
                    case Modifier.ShopPrice:
                        classStat.text = "Divide by " + classInfo.classStats[statIndex].value + " the prices in the shop";
                        break;
                    default:
                        classStat.text = "";
                        break;
                }*/
            }
        }
    }
}
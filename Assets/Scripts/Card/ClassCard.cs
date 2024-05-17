using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class ClassCard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI className = null;

        [SerializeField]
        private TextMeshProUGUI classDesc = null;

        [SerializeField]
        private TextMeshProUGUI classStat = null;

        public void SetClass(ClassInfo classInfo)
        {
            className.text = classInfo.className;
            classDesc.text = classInfo.classDescription;
            if (classInfo.classStats.Length == 0)
            {
                classStat.text = classInfo.classStatDescription;
                return;
            }

            for (int statIndex = 0; statIndex < classInfo.classStats.Length; statIndex++)
            {
                string statDesc = classInfo.classStatDescription.Replace("%%", classInfo.classStats[statIndex].value.ToString());

                classStat.text = statDesc;
            }
        }
    }
}
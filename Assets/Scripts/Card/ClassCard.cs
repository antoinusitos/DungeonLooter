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

        public void SetClass(ClassInfo classInfo)
        {
            className.text = classInfo.className;
            classDesc.text = classInfo.classDescription;
        }
    }
}
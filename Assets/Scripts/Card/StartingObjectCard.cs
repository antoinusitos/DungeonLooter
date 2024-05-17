using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class StartingObjectCard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI objectName = null;

        [SerializeField]
        private TextMeshProUGUI objectDesc = null;

        [SerializeField]
        private TextMeshProUGUI objectStats = null;

        public void SetObject(ItemInfo itemInfo)
        {
            objectName.text = itemInfo.itemName;
            objectDesc.text = itemInfo.itemDescription;

            objectStats.text = "";
            for (int statIndex = 0; statIndex < itemInfo.itemStats.Length; statIndex++)
            {
                string statDesc = itemInfo.itemStatDescription.Replace("%%", itemInfo.itemStats[statIndex].value.ToString());

                objectStats.text += statDesc + "\n";
            }
        }
    }
}
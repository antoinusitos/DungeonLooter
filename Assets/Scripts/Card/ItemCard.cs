using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class ItemCard : Card
    {
        [SerializeField]
        private TextMeshProUGUI objectName = null;

        [SerializeField]
        private TextMeshProUGUI objectStats = null;

        [SerializeField]
        private Image objectSprite = null;

        public void SetObject(ItemInfo itemInfo)
        {
            objectName.text = itemInfo.itemName;
            objectSprite.sprite = itemInfo.itemImage;

            objectStats.text = "";
            for (int statIndex = 0; statIndex < itemInfo.itemStats.Length; statIndex++)
            {
                string statDesc = itemInfo.itemStatDescription.Replace("%%", itemInfo.itemStats[statIndex].value.ToString());

                objectStats.text += statDesc + "\n";
            }
        }
    }
}
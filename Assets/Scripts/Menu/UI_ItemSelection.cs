using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class UI_ItemSelection : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI itemName = null;

        [SerializeField]
        private TextMeshProUGUI itemDesc = null;

        [SerializeField]
        private TextMeshProUGUI itemStat = null;

        [SerializeField]
        private Image itemImage = null;

        public void SetItem(ItemInfo itemInfo)
        {
            itemName.text = itemInfo.itemName;
            itemDesc.text = itemInfo.itemDescription;
            itemImage.sprite = itemInfo.itemImage;
            itemStat.text = "";

            if (itemInfo.itemStats.Length == 0)
            {
                itemStat.text = itemInfo.itemStatDescription;
                return;
            }

            for (int statIndex = 0; statIndex < itemInfo.itemStats.Length;  statIndex++)
            {
                string statDesc = itemInfo.itemStatDescription.Replace("%%", itemInfo.itemStats[statIndex].value.ToString());

                itemStat.text = statDesc;
            }
        }
    }
}
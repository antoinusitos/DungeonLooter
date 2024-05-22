using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AG
{
    public class ItemCard : Card, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI objectName = null;

        [SerializeField]
        private TextMeshProUGUI objectStats = null;

        [SerializeField]
        private Image objectSprite = null;

        private ItemInfo itemInfo = null;

        private bool inPlayerEquipment = false;

        private bool isEquipped = false;

        public void SetObject(ItemInfo inItemInfo)
        {
            itemInfo = inItemInfo;
            objectName.text = itemInfo.itemName;
            objectSprite.sprite = itemInfo.itemImage;

            objectStats.text = "";
            for (int statIndex = 0; statIndex < itemInfo.itemStats.Length; statIndex++)
            {
                string statDesc = itemInfo.itemStatDescription.Replace("%%", itemInfo.itemStats[statIndex].value.ToString());

                objectStats.text += statDesc + "\n";
            }
        }

        public ItemInfo GetItemInfo()
        {
            return itemInfo;
        }

        public void SetInPlayerEquipment()
        {
            inPlayerEquipment = true;
        }

        public void TryToEquip()
        {
            PlayerManager.instance.GetPlayerInventory().EquipItem(this);
        }

        public void TryToUnequip()
        {
            PlayerManager.instance.GetPlayerInventory().UnequipItem(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if(inPlayerEquipment && !isEquipped)
                {
                    TryToEquip();
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (inPlayerEquipment)
                {
                    if(isEquipped)
                    {
                        TryToUnequip();
                    }
                    else
                    {
                        PlayerManager.instance.GetPlayerInventory().DestroyItem(this);
                    }
                }
            }
        }

        public void SetIsEquipped(bool newState)
        {
            isEquipped = newState;
        }
    }
}
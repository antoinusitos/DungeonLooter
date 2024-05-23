using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class PlayerInventory : MonoBehaviour
    {
        private List<ItemCard> inventory = new List<ItemCard>();

        private int inventoryLimit = 10;

        private ItemCard weapon = null;
        private ItemCard headItem = null;
        private ItemCard feetItem = null;
        private ItemCard bodyItem = null;
        private ItemCard ringItem = null;
        private ItemCard shieldItem = null;

        private int coins = 0; //This must be saved

        public void CleanInventory()
        {
            for(int inventoryIndex = 0; inventoryIndex < inventory.Count; inventoryIndex++)
            {
                Destroy(inventory[inventoryIndex].gameObject);
            }

            inventory.Clear();
            weapon = null;
            headItem = null;
            feetItem = null;
            bodyItem = null;
            ringItem = null;
            shieldItem = null;
        }

        public void AddItemToInventory(ItemCard newItem)
        {
            inventory.Add(newItem);
            newItem.transform.SetParent(DungeonUIManager.instance.GetInventoryPanel(), false);
        }

        public void EquipItem(ItemCard newItem)
        {
            bool canEquip = true;
            switch (newItem.GetItemInfo().equipmentType)
            {
                case EquipmentType.Boot:
                    SwapCardToInventory(feetItem);
                    feetItem = newItem;
                    break;
                case EquipmentType.Armor:
                    SwapCardToInventory(bodyItem);
                    bodyItem = newItem;
                    break;
                case EquipmentType.Ring:
                    SwapCardToInventory(ringItem);
                    ringItem = newItem;
                    break;
                case EquipmentType.Shield:
                    SwapCardToInventory(shieldItem);
                    shieldItem = newItem;
                    break;
                case EquipmentType.Sword:
                    SwapCardToInventory(weapon);
                    weapon = newItem;
                    break;
                case EquipmentType.Bow:
                    SwapCardToInventory(weapon);
                    weapon = newItem;
                    break;
                case EquipmentType.Tunic:
                    SwapCardToInventory(bodyItem);
                    bodyItem = newItem;
                    break;
                case EquipmentType.Staff:
                    SwapCardToInventory(weapon);
                    weapon = newItem;
                    break;
                case EquipmentType.Hammer:
                    SwapCardToInventory(weapon);
                    weapon = newItem;
                    break;
                case EquipmentType.Hat:
                    SwapCardToInventory(headItem);
                    headItem = newItem;
                    break;
                default:
                    canEquip = false;
                    break;
            }

            if(!canEquip)
            {
                return;
            }

            SwapCardToEquipped(newItem);
        }

        public void UnequipItem(ItemCard newItem)
        {
            switch (newItem.GetItemInfo().equipmentType)
            {
                case EquipmentType.Boot:
                    SwapCardToInventory(feetItem);
                    feetItem = null;
                    break;
                case EquipmentType.Armor:
                    SwapCardToInventory(bodyItem);
                    bodyItem = null;
                    break;
                case EquipmentType.Ring:
                    SwapCardToInventory(ringItem);
                    ringItem = null;
                    break;
                case EquipmentType.Shield:
                    SwapCardToInventory(shieldItem);
                    shieldItem = null;
                    break;
                case EquipmentType.Sword:
                    SwapCardToInventory(weapon);
                    weapon = null;
                    break;
                case EquipmentType.Bow:
                    SwapCardToInventory(weapon);
                    weapon = null;
                    break;
                case EquipmentType.Tunic:
                    SwapCardToInventory(bodyItem);
                    bodyItem = null;
                    break;
                case EquipmentType.Staff:
                    SwapCardToInventory(weapon);
                    weapon = null;
                    break;
                case EquipmentType.Hammer:
                    SwapCardToInventory(weapon);
                    weapon = null;
                    break;
                case EquipmentType.Hat:
                    SwapCardToInventory(headItem);
                    headItem = null;
                    break;
            }
        }

        public void DestroyItem(ItemCard newItem)
        {
            inventory.Remove(newItem);
            Destroy(newItem.gameObject);
        }

        private void SwapCardToInventory(ItemCard card)
        {
            if (card)
            {
                card.transform.SetParent(DungeonUIManager.instance.GetInventoryPanel(), false);
                card.SetIsEquipped(false);
                for (int statIndex = 0; statIndex < card.GetItemInfo().itemStats.Length; statIndex++)
                {
                    switch (card.GetItemInfo().itemStats[statIndex].modifier)
                    {
                        case Modifier.Damage:
                            PlayerManager.instance.ModifyDamage(-card.GetItemInfo().itemStats[statIndex].value);
                            break;
                    }
                }
            }
        }

        private void SwapCardToEquipped(ItemCard card)
        {
            if (card)
            {
                card.transform.SetParent(DungeonUIManager.instance.GetEquippedPanel(), false);
                card.SetIsEquipped(true);
                for (int statIndex = 0; statIndex < card.GetItemInfo().itemStats.Length; statIndex++)
                {
                    switch (card.GetItemInfo().itemStats[statIndex].modifier)
                    {
                        case Modifier.Damage:
                            PlayerManager.instance.ModifyDamage(card.GetItemInfo().itemStats[statIndex].value);
                            break;
                    }
                }
            }
        }

        public void AddCoin(int value)
        {
            coins += value;
            DungeonUIManager.instance.SetCoinsText(coins);
        }

        public void ActivateCards()
        {
            for (int inventoryIndex = 0; inventoryIndex < inventory.Count; inventoryIndex++)
            {
                if (inventory[inventoryIndex].GetCardType() == CardType.Equipment)
                {
                    continue;
                }
                Button buttonInventory = inventory[inventoryIndex].GetComponent<Button>();
                if (buttonInventory)
                {
                    buttonInventory.interactable = true;
                }
            }

            Button button = DungeonUIManager.instance.GetStartingObjectCard().GetComponent<Button>();
            if (button)
            {
                button.interactable = true;
            }
        }

        public void DesactivateCards()
        {
            for (int inventoryIndex = 0; inventoryIndex < inventory.Count; inventoryIndex++)
            {
                if(inventory[inventoryIndex].GetCardType() == CardType.Equipment)
                {
                    continue;
                }
                Button buttonInventory = inventory[inventoryIndex].GetComponent<Button>();
                if (buttonInventory)
                {
                    buttonInventory.interactable = false;
                }
            }

            Button button = DungeonUIManager.instance.GetStartingObjectCard().GetComponent<Button>();
            if (button)
            {
                button.interactable = false;
            }
        }
    }
}
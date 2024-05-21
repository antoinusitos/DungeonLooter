using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class PlayerInventory : MonoBehaviour
    {
        private List<ItemInfo> inventory = new List<ItemInfo>();

        private int inventoryLimit = 10;

        public void AddItemToInventory(ItemInfo newItem)
        {
            inventory.Add(newItem);
        }
    }
}
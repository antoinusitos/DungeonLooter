using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Items/Infos/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public EquipmentType equipmentType = EquipmentType.None;

        public string itemName = "";
        public string itemDescription = "";

        public Sprite itemImage = null;

        public string itemStatDescription = "";
        public Stat[] itemStats = null;
    }
}
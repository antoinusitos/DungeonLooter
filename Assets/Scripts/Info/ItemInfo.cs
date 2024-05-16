using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Creation/Infos/Item")]
    public class ItemInfo : ScriptableObject
    {
        public StartingObject itemClass = StartingObject.None;

        public string itemName = "";
        public string itemDescription = "";

        public Sprite itemImage = null;

        public string itemStatDescription = "";
        public Stat[] itemStats = null;
    }
}
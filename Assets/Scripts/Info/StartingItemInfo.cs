using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Creation/Infos/Item")]
    public class StartingItemInfo : ItemInfo
    {
        public StartingObject itemClass = StartingObject.None;
    }
}
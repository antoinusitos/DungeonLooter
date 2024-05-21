using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance = null;

        [SerializeField]
        private ItemInfo[] equipments = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public ItemInfo GetRandomEquipment()
        {
            return equipments[Random.Range(0, equipments.Length)];
        }
    }
}
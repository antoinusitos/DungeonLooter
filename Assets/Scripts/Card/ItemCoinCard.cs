using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class ItemCoinCard : Card
    {
        [SerializeField]
        private TextMeshProUGUI coinsValue = null;

        public void SetValue(int newCoins)
        {
            coinsValue.text = newCoins.ToString();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private CardType cardType = CardType.None;

        public void SendCardType()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().ReceiveCardType(cardType);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class CardsManager : MonoBehaviour
    {
        public static CardsManager instance = null;

        public Card cardPrefab = null;

        [Header("End Room")]
        public Card cardLeftPrefab = null;
        public Card cardRightPrefab = null;
        public Card cardTopPrefab = null;
        public Card cardBottomPrefab = null;

        [Header("Start Room")]
        public Card cardObservePrefab = null;
        public Card cardEnterPrefab = null;
        public Card cardDescriptionPrefab = null;

        [Header("Door")]
        public Card cardOpenDoorPrefab = null;
        public Card cardRunAwayDoorPrefab = null;

        [Header("Turn Around")]
        public Card cardTurnAroundPrefab = null;

        [Header("Monster")]
        public Card cardMonsterPrefab = null;
        public Card cardAttackPrefab = null;
        public Card cardPlayerPrefab = null;

        [Header("Shared")]
        public Card cardUseObjectPrefab = null;
        public Card cardRunAwayPrefab = null;
        public Card cardContinuePrefab = null;
        public Card cardLootPrefab = null;
        public Card cardNotLootPrefab = null;
        public Card cardItemPrefab = null;
        public Card cardItemCoinPrefab = null;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
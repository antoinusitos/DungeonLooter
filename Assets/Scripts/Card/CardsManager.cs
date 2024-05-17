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
        public Card cardRunAwayPrefab = null;
        public Card cardDescriptionPrefab = null;

        [Header("Door")]
        public Card cardUseObjectPrefab = null;
        public Card cardOpenDoorPrefab = null;
        public Card cardRunAwayDoorPrefab = null;

        [Header("Turn Around")]
        public Card cardTurnAroundPrefab = null;

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
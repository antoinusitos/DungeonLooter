using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonUIManager : MonoBehaviour
    {
        public static DungeonUIManager instance = null;

        [SerializeField]
        private Transform gameUI = null;

        [SerializeField]
        private Transform cardPlacementPanel = null;

        [SerializeField]
        private Transform cardPlacementDescription = null;

        [SerializeField]
        private RaceCard raceCard = null;
        [SerializeField]
        private ClassCard classCard = null;
        [SerializeField]
        private StartingObjectCard startingObjectCard = null;

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

        public Transform GetGameUI()
        {
            return gameUI;
        }

        public Transform GetCardPlacementPanel()
        {
            return cardPlacementPanel;
        }

        public Transform GetCardPlacementDescription()
        {
            return cardPlacementDescription;
        }

        public void CleanGameUI()
        {
            for(int cardIndex = 0; cardIndex < cardPlacementPanel.childCount; cardIndex++)
            {
                Destroy(cardPlacementPanel.GetChild(cardIndex).gameObject);
            }
        }

        public void CleanDescriptionCard()
        {
            Destroy(cardPlacementDescription.GetChild(0).gameObject);
        }

        public RaceCard GetRaceCard()
        {
            return raceCard;
        }

        public ClassCard GetClassCard()
        {
            return classCard;
        }

        public StartingObjectCard GetStartingObjectCard()
        {
            return startingObjectCard;
        }
    }
}
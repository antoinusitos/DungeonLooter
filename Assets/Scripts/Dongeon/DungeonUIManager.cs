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

        [Header("Loading")]
        [SerializeField]
        private RectTransform loadingImage = null;
        private float loadingShowSpeed = 2.0f;

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

        public void ShowLoadingScreen()
        {
            StartCoroutine("ShowingLoadingScreen");
        }

        private IEnumerator ShowingLoadingScreen()
        {
            float timer = 0f;
            while (timer < 1)
            {
                loadingImage.anchoredPosition = new Vector2(0, 1080.0f - (timer * 1080.0f));
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
            }

            loadingImage.anchoredPosition = Vector2.zero;

            FindObjectOfType<StartScreen>().gameObject.SetActive(false);

             DungeonGeneratorManager.instance.GenerateDungeon();

            yield return new WaitForSeconds(2);

            timer = 0f;
            while (timer < 1)
            {
                loadingImage.anchoredPosition = new Vector2(0, timer * 1080.0f);
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
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
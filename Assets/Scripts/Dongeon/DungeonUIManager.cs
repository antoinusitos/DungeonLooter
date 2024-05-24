using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class DungeonUIManager : MonoBehaviour
    {
        public static DungeonUIManager instance = null;

        private StartScreen startScreen = null;

        [SerializeField]
        private Transform gameUI = null;

        [SerializeField]
        private Transform cardPlacementPanel = null;

        [SerializeField]
        private Transform cardPlacementDescription = null;

        [SerializeField]
        private Transform cardPlacementScore = null;

        [SerializeField]
        private Transform monsterPlacementPanel = null;
        [SerializeField]
        private Transform playerPlacementPanel = null;

        [SerializeField]
        private RaceCard raceCard = null;
        [SerializeField]
        private ClassCard classCard = null;
        [SerializeField]
        private StartingObjectCard startingObjectCard = null;

        [Header("Loading")]
        [SerializeField]
        private RectTransform loadingImage = null;
        private const float loadingShowSpeed = 2.0f;

        [Header("Loading")]
        [SerializeField]
        private RectTransform deadImage = null;

        [Header("Inventory")]
        [SerializeField]
        private Transform inventoryPanel = null;
        [SerializeField]
        private Transform equippedPanel = null;

        [Header("Inventory")]
        [SerializeField]
        private TextMeshProUGUI hpText = null;
        [SerializeField]
        private TextMeshProUGUI damageText = null;
        [SerializeField]
        private TextMeshProUGUI chanceText = null;
        [SerializeField]
        private TextMeshProUGUI perceptionText = null;
        [SerializeField]
        private TextMeshProUGUI coinsText = null;

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

            if(!startScreen)
            {
                startScreen = FindObjectOfType<StartScreen>();
            }
            startScreen.gameObject.SetActive(false);

             DungeonGeneratorManager.instance.GenerateDungeon();

            yield return new WaitForSeconds(2);

            timer = 0f;
            while (timer < 1)
            {
                loadingImage.anchoredPosition = new Vector2(0, timer * 1080.0f);
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
            }

            loadingImage.anchoredPosition = new Vector2(0, 1081.0f);
        }

        public void ShowEndLoadingScreen()
        {
            StartCoroutine("ShowingEndLoadingScreen");
        }

        private IEnumerator ShowingEndLoadingScreen()
        {
            float timer = 0f;
            while (timer < 1)
            {
                deadImage.anchoredPosition = new Vector2(0, 1080.0f - (timer * 1080.0f));
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
            }

            yield return new WaitForSeconds(1);

            timer = 0f;
            while (timer < 1)
            {
                loadingImage.anchoredPosition = new Vector2(0, 1080.0f - (timer * 1080.0f));
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
            }

            loadingImage.anchoredPosition = Vector2.zero;

            CleanDescriptionCard();
            CleanGameUI();
            CleanScoreCard();
            CleanMonsterCard();
            CleanPlayerCard();

            DungeonGeneratorManager.instance.DestroyDungeon();

            startScreen.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            timer = 0f;
            while (timer < 1)
            {
                deadImage.anchoredPosition = new Vector2(0, timer * 1080.0f);
                loadingImage.anchoredPosition = new Vector2(0, timer * 1080.0f);
                timer += Time.deltaTime * loadingShowSpeed;
                yield return null;
            }

            deadImage.anchoredPosition = new Vector2(0, 1081.0f);
            loadingImage.anchoredPosition = new Vector2(0, 1081.0f);
        }

        public Transform GetGameUI()
        {
            return gameUI;
        }

        public Transform GetCardPlacementPanel()
        {
            return cardPlacementPanel;
        }

        public Transform GetMonsterPlacementPanel()
        {
            return monsterPlacementPanel;
        }

        public Transform GetCardPlacementDescription()
        {
            return cardPlacementDescription;
        }

        public Transform GetPlayerPlacementPanel()
        {
            return playerPlacementPanel;
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
            if(cardPlacementDescription.childCount > 0)
            {
                Destroy(cardPlacementDescription.GetChild(0).gameObject);
            }
        }

        public void CleanMonsterCard()
        {
            if (monsterPlacementPanel.childCount > 0)
            {
                Destroy(monsterPlacementPanel.GetChild(0).gameObject);
            }
        }

        public void CleanPlayerCard()
        {
            if (playerPlacementPanel.childCount > 0)
            {
                Destroy(playerPlacementPanel.GetChild(0).gameObject);
            }
        }

        public void CleanScoreCard()
        {
            if(cardPlacementScore.childCount> 0)
            {
                Destroy(cardPlacementScore.GetChild(0).gameObject);
            }
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

        public Transform GetCardPlacementScore()
        {
            return cardPlacementScore;
        }

        public Transform GetInventoryPanel()
        {
            return inventoryPanel;
        }

        public Transform GetEquippedPanel()
        {
            return equippedPanel;
        }

        public void SetDamageText(float value)
        {
            damageText.text = value.ToString();
        }

        public void SetChanceText(float value)
        {
            chanceText.text = value.ToString();
        }

        public void SetPerceptionText(float value)
        {
            perceptionText.text = value.ToString();
        }

        public void SetCoinsText(float value)
        {
            coinsText.text = value.ToString();
        }

        public void SetHPText(float value, float max)
        {
            hpText.text = value.ToString() + "/" + max.ToString();
        }
    }
}
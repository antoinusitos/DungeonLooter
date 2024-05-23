using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AG
{
    public class StartingObjectCard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI objectName = null;

        [SerializeField]
        private TextMeshProUGUI objectDesc = null;

        [SerializeField]
        private TextMeshProUGUI objectStats = null;

        [SerializeField]
        private Image objectSprite = null;

        public void SetObject(StartingItemInfo itemInfo)
        {
            objectName.text = itemInfo.itemName;
            objectDesc.text = itemInfo.itemDescription;
            objectSprite.sprite = itemInfo.itemImage;

            objectStats.text = "";
            for (int statIndex = 0; statIndex < itemInfo.itemStats.Length; statIndex++)
            {
                string statDesc = itemInfo.itemStatDescription.Replace("%%", itemInfo.itemStats[statIndex].value.ToString());

                objectStats.text += statDesc + "\n";
            }

            switch(itemInfo.itemClass)
            {
                case StartingObject.SealedLetter:
                    {
                        Card card = gameObject.AddComponent<Card>();
                        card.SetCardType(CardType.SealedLetter);
                        Button button = GetComponent<Button>();
                        if (!button)
                        {
                            button = gameObject.AddComponent<Button>();
                        }
                        else
                        {
                            button.onClick.RemoveAllListeners();
                        }
                        button.interactable = false;
                        button.onClick.AddListener(delegate { card.SendCardType(); });
                    }
                    break;
                case StartingObject.HealPotion:
                    {
                        Button button = GetComponent<Button>();
                        if (!button)
                        {
                            button = gameObject.AddComponent<Button>();
                        }
                        else
                        {
                            button.onClick.RemoveAllListeners();
                        }
                        button.onClick.AddListener(delegate { 
                            PlayerManager.instance.RefillHP(itemInfo.itemStats[0].value); 
                            Destroy(DungeonUIManager.instance.GetStartingObjectCard().gameObject); 
                        });
                    }
                    break;
            }
        }
    }
}
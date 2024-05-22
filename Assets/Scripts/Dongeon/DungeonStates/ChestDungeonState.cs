using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/ChestDungeonState")]
    public class ChestDungeonState : DungeonState
    {
        private ItemInfo loot = null;
        private bool looted = false;

        private int coins = 0;

        public override void OnStateEnter()
        {
            looted = false;
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Card lootCard = Instantiate(CardsManager.instance.cardItemPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            loot = EquipmentManager.instance.GetRandomEquipment();
            lootCard.GetComponent<ItemCard>().SetObject(loot);

            Instantiate(CardsManager.instance.cardLootPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            Instantiate(CardsManager.instance.cardNotLootPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            coins = Random.Range(0, 3);
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Loot:
                    {
                        if (!looted)
                        {
                            looted = true;
                            Card lootInstance = Instantiate(CardsManager.instance.cardItemPrefab);
                            ItemCard itemCard = lootInstance.GetComponent<ItemCard>();
                            itemCard.SetObject(loot);
                            itemCard.SetInPlayerEquipment();
                            PlayerManager.instance.GetPlayerInventory().AddItemToInventory(itemCard);

                            DungeonUIManager.instance.CleanDescriptionCard();

                            if (coins > 0)
                            {
                                Card lootCard = Instantiate(CardsManager.instance.cardItemCoinPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                                lootCard.GetComponent<ItemCoinCard>().SetValue(coins);
                                return null;
                            }
                        }
                        else
                        {
                            PlayerManager.instance.GetPlayerInventory().AddCoin(coins);
                        }

                        DungeonUIManager.instance.CleanDescriptionCard();
                        DungeonUIManager.instance.CleanGameUI();

                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Which way to go to ?";

                        return DungeonStatesManager.instance.endRoomDungeonStateInstance;
                    }
                case CardType.NotLoot:
                    if(!looted)
                    {
                        looted = true;
                        DungeonUIManager.instance.CleanDescriptionCard();
                        if (coins > 0)
                        {
                            Card lootCard = Instantiate(CardsManager.instance.cardItemCoinPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                            lootCard.GetComponent<ItemCoinCard>().SetValue(coins);
                            return null;
                        }

                        DungeonUIManager.instance.CleanGameUI();

                        Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                        cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Which way to go to ?";

                        return DungeonStatesManager.instance.endRoomDungeonStateInstance;
                    }
                    break;
            }
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
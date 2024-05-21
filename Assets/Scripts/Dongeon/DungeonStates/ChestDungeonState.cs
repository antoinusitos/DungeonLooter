using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/ChestDungeonState")]
    public class ChestDungeonState : DungeonState
    {
        private bool looted = false;
        private ItemInfo loot = null;

        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Card lootCard = Instantiate(CardsManager.instance.cardItemPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            loot = EquipmentManager.instance.GetRandomEquipment();
            lootCard.GetComponent<ItemCard>().SetObject(loot);

            Instantiate(CardsManager.instance.cardLootPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
            Instantiate(CardsManager.instance.cardNotLootPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Continue:
                    {
                        if(looted)
                        {
                            DungeonUIManager.instance.CleanDescriptionCard();
                            DungeonUIManager.instance.CleanGameUI();

                            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "Which way to go to ?";

                            return DungeonStatesManager.instance.endRoomDungeonStateInstance;
                        }
                    }
                    break;
                case CardType.Loot:
                    {
                        looted = true;
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
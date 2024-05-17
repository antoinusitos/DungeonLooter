using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/EventDungeonState")]
    public class EventDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "EVENT !";
            DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.endRoomDungeonStateInstance);
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
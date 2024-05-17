using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/TurnAroundDungeonState")]
    public class TurnAroundDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();
            DungeonUIManager.instance.CleanDescriptionCard();

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You run away, making a lot of noise trying to open the one way door.\nA shadow appears behind you.";

            Instantiate(CardsManager.instance.cardTurnAroundPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.TurnAround:
                    DungeonGeneratorManager.instance.GetDungeon().SetPreviousDoor(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor());
                    DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                    DungeonUIManager.instance.CleanDescriptionCard();
                    return DungeonStatesManager.instance.inRoomDungeonStateInstance;
            }
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}

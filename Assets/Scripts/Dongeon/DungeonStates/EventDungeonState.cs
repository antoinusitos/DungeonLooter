using System.Collections;
using System.Collections.Generic;
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

            DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonEventsManager.instance.eventsInstance[Random.Range(0, DungeonEventsManager.instance.eventsInstance.Length)]);
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
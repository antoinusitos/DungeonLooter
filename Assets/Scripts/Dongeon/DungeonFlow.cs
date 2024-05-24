using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class DungeonFlow : MonoBehaviour
    {
        private DungeonState currentDungeonState = null;

        public void StartFlow()
        {
            SwitchToState(Instantiate(DungeonStatesManager.instance.startRoomDungeonStateInstance));
        }

        public void SwitchToState(DungeonState newState)
        {
            if(currentDungeonState)
            {
                currentDungeonState.OnStateExit();
            }
            currentDungeonState = newState;
            if(currentDungeonState)
            {
                currentDungeonState.OnStateEnter();
            }
        }

        private void Update()
        {
            if (currentDungeonState)
            {
                currentDungeonState.OnStateUpdate();
            }
        }

        public void ReceiveCardType(CardType cardType)
        {
            if(currentDungeonState)
            {
                DungeonState stateToGoTo = currentDungeonState.ReceiveCardType(cardType);
                if(stateToGoTo)
                {
                    SwitchToState(stateToGoTo);
                }
            }
        }
    }
}
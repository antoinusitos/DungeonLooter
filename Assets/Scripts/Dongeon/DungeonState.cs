using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class DungeonState : ScriptableObject
    {
        protected void CleanGameUI()
        {
            DungeonUIManager.instance.CleanGameUI();
        }

        public virtual void OnStateEnter()
        {

        }

        public virtual DungeonState ReceiveCardType(CardType cardType)
        {
            return null;
        }

        public virtual void OnStateUpdate()
        {

        }

        public virtual void OnStateExit()
        {

        }
    }
}
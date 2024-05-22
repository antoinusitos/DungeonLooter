using AG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class CombatAnimationManager : MonoBehaviour
    {
        public static CombatAnimationManager instance = null;

        private void Start()
        {
            instance = this;
        }

        public void Attack(PlayerCard playerCard, Monster currentMonster)
        {
            StartCoroutine(AttackFlow(playerCard, currentMonster));
        }

        private IEnumerator AttackFlow(PlayerCard playerCard, Monster currentMonster)
        {
            playerCard.Attack();
            while(!playerCard.GetMovementDone())
            { 
                yield return null;
            }
            currentMonster.TakeDamage(PlayerManager.instance.GetDamage());
            if (currentMonster.GetCurrentHP() <= 0)
            {
                DungeonUIManager.instance.CleanDescriptionCard();
                DungeonUIManager.instance.CleanMonsterCard();
                DungeonUIManager.instance.CleanPlayerCard();

                DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom().SetRoomType(RoomType.Empty);

                DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.chestDungeonStateInstance);
            }

            currentMonster.Attack();
            while (!currentMonster.GetMovementDone())
            {
                yield return null;
            }
            //monster reply
            PlayerManager.instance.TakeDamage(currentMonster.GetDamage());
            playerCard.SetHP(PlayerManager.instance.GetCurrentHP());
        }
    }
}
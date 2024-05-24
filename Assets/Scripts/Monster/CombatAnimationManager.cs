using AG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class CombatAnimationManager : MonoBehaviour
    {
        public static CombatAnimationManager instance = null;

        private bool isAttacking = false;

        private void Start()
        {
            instance = this;
        }

        public void Attack(PlayerCard playerCard, Monster currentMonster)
        {
            isAttacking = true;
            StartCoroutine(AttackFlow(playerCard, currentMonster));
        }

        private IEnumerator AttackFlow(PlayerCard playerCard, Monster currentMonster)
        {
            Card score = Instantiate(CardsManager.instance.cardScorePrefab, DungeonUIManager.instance.GetCardPlacementScore());
            ScoreCard scoreCard = score.GetComponent<ScoreCard>();

            float timer = 0;
            int randTime = 0;
            int rand = 0;
            while (timer < 1 && randTime < 10)
            {
                if (timer >= 0.1f)
                {
                    rand = Random.Range(0, 100);
                    scoreCard.SetNumber(rand);
                    if (rand < PlayerManager.instance.GetCurrentChance())
                    {
                        //player miss
                        scoreCard.SetColor(Color.red);
                        scoreCard.SetDescription("Missed Attack");
                    }
                    else if (rand > (95 - PlayerManager.instance.GetCurrentChance()))
                    {
                        //damage x2
                        scoreCard.SetColor(Color.green);
                        scoreCard.SetDescription("Damage x2");
                    }
                    else
                    {
                        scoreCard.SetColor(Color.black);
                        scoreCard.SetDescription("");
                    }
                    timer = 0;
                    randTime++;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1);

            playerCard.Attack();
            while(!playerCard.GetMovementDone())
            { 
                yield return null;
            }
            float damage = PlayerManager.instance.GetDamage();
            if(rand < 5)
            {
                //player miss
                damage = 0;
            }
            else if(rand > (95 - PlayerManager.instance.GetCurrentChance()))
            {
                //damage x2
                damage *= 2;
            }
            currentMonster.TakeDamage(damage);
            if (currentMonster.GetCurrentHP() <= 0)
            {
                DungeonUIManager.instance.CleanDescriptionCard();
                DungeonUIManager.instance.CleanMonsterCard();
                DungeonUIManager.instance.CleanPlayerCard();
                DungeonUIManager.instance.CleanScoreCard();

                DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom().SetRoomType(RoomType.Empty);

                DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.chestDungeonStateInstance);
                isAttacking = false;
            }
            else
            {
                DungeonUIManager.instance.CleanScoreCard();

                yield return new WaitForSeconds(2);

                score = Instantiate(CardsManager.instance.cardScorePrefab, DungeonUIManager.instance.GetCardPlacementScore());
                scoreCard = score.GetComponent<ScoreCard>();

                timer = 0;
                randTime = 0;
                rand = 0;
                while (timer < 1 && randTime < 10)
                {
                    if (timer >= 0.1f)
                    {
                        rand = Random.Range(0, 100);
                        scoreCard.SetNumber(rand);
                        if (rand < currentMonster.GetCurrentChance())
                        {
                            //monster miss
                            scoreCard.SetColor(Color.red);
                            scoreCard.SetDescription("Missed Attack");
                        }
                        else if (rand > 100 - currentMonster.GetCurrentChance())
                        {
                            //damage x2
                            scoreCard.SetColor(Color.green);
                            scoreCard.SetDescription("Damage x2");
                        }
                        else
                        {
                            scoreCard.SetColor(Color.black);
                            scoreCard.SetDescription("");
                        }
                        timer = 0;
                        randTime++;
                    }

                    timer += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(1);

                currentMonster.Attack();
                while (!currentMonster.GetMovementDone())
                {
                    yield return null;
                }
                //monster reply

                damage = currentMonster.GetDamage();
                if (rand < currentMonster.GetCurrentChance())
                {
                    //monster miss
                    damage = 0;
                }
                else if (rand > 100 - currentMonster.GetCurrentChance())
                {
                    //monster x2
                    damage *= 2;
                }

                PlayerManager.instance.TakeDamage(damage);
                playerCard.SetHP(PlayerManager.instance.GetCurrentHP());
                DungeonUIManager.instance.CleanScoreCard();

                isAttacking = false;
            }
        }

        public bool GetIsAttacking()
        {
            return isAttacking;
        }
    }
}
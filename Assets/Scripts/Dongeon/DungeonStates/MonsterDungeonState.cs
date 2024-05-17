using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/MonsterDungeonState")]
    public class MonsterDungeonState : DungeonState
    {
        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonUIManager.instance.CleanDescriptionCard();

            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            Room currentRoom = DungeonGeneratorManager.instance.GetDungeon().GetCurrentRoom();

            if(currentRoom.GetRoomType() == RoomType.Boss)
            {
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "BOSS !";
            }
            else
            {
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "MONSTER !";
            }
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
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Dungeon/States/DoorDungeonState")]
    public class DoorDungeonState : DungeonState
    {
        private const float trappedDoorDamage = 5;

        public override void OnStateEnter()
        {
            CleanGameUI();

            DungeonGeneratorManager.instance.GetDungeon().SetCurrentRoom(null);

            DungeonUIManager.instance.CleanDescriptionCard();
            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text = "You face a " + DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor().GetDoorMaterial() + " door.";

            bool skipDoor = true;
            if(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor().GetDoorDirection() == DoorDirection.MonoDirectionnal)
            {
                skipDoor = false;
                cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "\nYou see that the door is one way.";
            }

            if(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor().GetDoorType() == DoorType.Trapped)
            {
                float rand = Random.Range(0f, 100f);
                if(rand <= PlayerManager.instance.GetCurrentPerception())
                {
                    skipDoor = false;
                    cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "\nYour senses tell you that this door is trapped";
                }
            }

            if(skipDoor)
            {
                DungeonUIManager.instance.CleanDescriptionCard();
                //Handle Event
                DungeonGeneratorManager.instance.GetDungeon().SetPreviousDoor(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor());
                DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                DungeonUIManager.instance.CleanDescriptionCard();
                DungeonGeneratorManager.instance.GetDungeonFlow().SwitchToState(DungeonStatesManager.instance.startRoomDungeonStateInstance);
                return;
            }

            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "\n\nWhat do you do ?";

            Instantiate(CardsManager.instance.cardObservePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardUseObjectPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardOpenDoorPrefab, DungeonUIManager.instance.GetCardPlacementPanel());

            Instantiate(CardsManager.instance.cardRunAwayDoorPrefab, DungeonUIManager.instance.GetCardPlacementPanel());
        }

        public override DungeonState ReceiveCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.OpenDoor:
                    {
                        //Handle Event
                        DungeonGeneratorManager.instance.GetDungeon().SetPreviousDoor(DungeonGeneratorManager.instance.GetDungeon().GetTargetDoor());
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                        DungeonUIManager.instance.CleanDescriptionCard();


                        float rand = Random.Range(0f, 100f);
                        if (rand <= PlayerManager.instance.GetCurrentPerception())
                        {
                            Card cardDesc = Instantiate(CardsManager.instance.cardDescriptionPrefab, DungeonUIManager.instance.GetCardPlacementDescription());
                            cardDesc.GetComponentInChildren<TextMeshProUGUI>().text += "While entering the room, you take some damage";

                            PlayerManager.instance.TakeDamage(trappedDoorDamage);

                            Instantiate(CardsManager.instance.cardContinuePrefab, DungeonUIManager.instance.GetCardPlacementPanel());

                            return null;
                        }

                        return DungeonStatesManager.instance.startRoomDungeonStateInstance;
                    }
                case CardType.RunAwayDoor:
                    {
                        DungeonGeneratorManager.instance.GetDungeon().SetNextRoom(DungeonGeneratorManager.instance.GetDungeon().GetPreviousRoom());
                        DungeonGeneratorManager.instance.GetDungeon().SetTargetDoor(null);
                        DungeonUIManager.instance.CleanDescriptionCard();
                        return DungeonStatesManager.instance.startRoomDungeonStateInstance;
                    }
                case CardType.Continue:
                    return DungeonStatesManager.instance.startRoomDungeonStateInstance;
            }
            return null;
        }

        public override void OnStateExit()
        {
        }
    }
}
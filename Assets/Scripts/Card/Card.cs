using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Card : MonoBehaviour
    {
        public void GoLeft()
        { 
            DungeonGeneratorManager.instance.GetDungeonFlow().GoLeft();
        }

        public void GoRight()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().GoRight();
        }

        public void GoTop()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().GoTop();
        }

        public void GoBottom()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().GoBottom();
        }

        public void EnterRoom()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().EnterRoom();
        }
    }
}
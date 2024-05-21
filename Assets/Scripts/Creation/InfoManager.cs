using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    //Hold all the races, classes and objects
    public class InfoManager : MonoBehaviour
    {
        public static InfoManager instance = null;

        [SerializeField]
        private RaceInfo[] races = null;

        [SerializeField]
        private ClassInfo[] classes = null;

        [SerializeField]
        private StartingItemInfo[] items = null;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int GetRacesNumber()
        {
            return races.Length;
        }

        public RaceInfo GetRaceInfoWithIndex(int index)
        {
            return races[index];
        }

        public int GetClassesNumber()
        {
            return classes.Length;
        }

        public ClassInfo GetClassInfoWithIndex(int index)
        {
            return classes[index];
        }

        public int GetItemsNumber()
        {
            return items.Length;
        }

        public StartingItemInfo GetItemInfoWithIndex(int index)
        {
            return items[index];
        }
    }
}
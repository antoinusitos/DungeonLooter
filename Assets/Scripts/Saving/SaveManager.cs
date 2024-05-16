using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance = null;

        public RaceAvailability[] races = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
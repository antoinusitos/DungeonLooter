using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    [CreateAssetMenu(menuName = "Creation/Infos/Class")]
    public class ClassInfo : ScriptableObject
    {
        public PlayerClass playerClass = PlayerClass.None;

        public string className = "";
        public string classDescription = "";

        public Sprite classImage = null;

        public string classStatDescription = "";
        public Stat[] classStats = null;
    }
}
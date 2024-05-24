using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AG
{
    public class ScoreCard : Card
    {
        [SerializeField]
        private TextMeshProUGUI scoreText = null;
        [SerializeField]
        private TextMeshProUGUI descriptionText = null;

        public void SetNumber(int number)
        {
            scoreText.text = number.ToString();
        }

        public void SetDescription(string description)
        {
            descriptionText.text = description;
        }

        public void SetColor(Color color)
        {
            scoreText.color = color;
        }
    }
}
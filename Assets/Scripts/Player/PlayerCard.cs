using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace AG
{
    public class PlayerCard : MonoBehaviour
    {
        [SerializeField]
        private RawImage playerSprite = null;
        [SerializeField]
        private TextMeshProUGUI playerNameText = null;
        [SerializeField]
        private TextMeshProUGUI playerHPText = null;

        private Vector3 startingPos = Vector3.zero;

        private const float cardSpeed = 5.0f;
        private const float offset = 300.0f;

        private bool movementDone = true;

        public void Setup(float hp, Sprite sprite)
        {
            playerSprite.texture = sprite.texture;
            playerHPText.text = hp.ToString();
        }

        public void SetHP(float hp)
        {
            playerHPText.text = hp.ToString();
        }

        public void Attack()
        {
            startingPos = transform.position;
            movementDone = false;
            StartCoroutine(PlayAttackAnimation());
        }

        private IEnumerator PlayAttackAnimation()
        {
            float timer = 0;
            while(timer < 1)
            {
                transform.position = Vector3.Lerp(startingPos, startingPos + Vector3.up * offset, timer);
                timer += Time.deltaTime * cardSpeed;
                yield return null;
            }
            timer = 0;
            while (timer < 1)
            {
                transform.position = Vector3.Lerp(startingPos + Vector3.up * 2, startingPos, timer);
                timer += Time.deltaTime * cardSpeed;
                yield return null;
            }

            transform.position = startingPos;
            movementDone = true;
        }

        public bool GetMovementDone()
        {
            return movementDone;
        }
    }
}
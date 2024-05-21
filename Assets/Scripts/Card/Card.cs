using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private CardType cardType = CardType.None;

        private RectTransform rectTransform = null;

        [SerializeField]
        private float speed = 2.0f;

        [SerializeField]
        private GameObject upFace = null;
        [SerializeField]
        private GameObject downFace = null;

        [SerializeField]
        private GameObject infosGameObject = null;

        private bool showUpFace = true;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if(!rectTransform)
            {
                rectTransform = GetComponent<RectTransform>();
                upFace = transform.GetChild(0).gameObject;
                downFace = transform.GetChild(1).gameObject;

                downFace.SetActive(false);
            }
        }

        public void ShowDownFace()
        {
            Init();
            rectTransform.rotation = Quaternion.Euler(0, 180, 0);
            showUpFace = false;
            infosGameObject.SetActive(false);
            downFace.SetActive(true);
            upFace.SetActive(false);
        }

        public void ShowUpFace()
        {
            Init();
            rectTransform.rotation = Quaternion.identity;
            showUpFace = true;
            infosGameObject.SetActive(true);
            downFace.SetActive(false);
            upFace.SetActive(true);
        }

        public void ReturnCard()
        {
            Init();
            StartCoroutine(ReturnCardAnim());
        }

        private IEnumerator ReturnCardAnim()
        {
            float timer = 0;
            if (showUpFace)
            {
                while (timer < 1)
                {
                    if(timer >= 0.5)
                    {
                        infosGameObject.SetActive(false);
                        downFace.SetActive(true);
                        upFace.SetActive(false);
                    }
                    rectTransform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 180, timer), 0);
                    timer += Time.deltaTime;
                    yield return null;
                }
                showUpFace = false;
            }
            else
            {
                while (timer < 1)
                {
                    if (timer >= 0.5)
                    {
                        infosGameObject.SetActive(true);
                        downFace.SetActive(false);
                        upFace.SetActive(true);
                    }
                    rectTransform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 0, timer), 0);
                    timer += Time.deltaTime;
                    yield return null;
                }
                showUpFace = true;
            }
        }

        public void SendCardType()
        {
            DungeonGeneratorManager.instance.GetDungeonFlow().ReceiveCardType(cardType);
        }


    }
}
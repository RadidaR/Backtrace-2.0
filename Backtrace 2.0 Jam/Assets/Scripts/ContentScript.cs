using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MPUIKIT;
using MEC;

namespace MMC
{
    public class ContentScript : MonoBehaviour
    {
        public ContentObject content;
        public Side side;
        public int placeInQueue;
        public bool chosen;

        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;
        public MPImage graphic;
        public TextMeshProUGUI physicalText;
        public TextMeshProUGUI mentalText;
        public TextMeshProUGUI spiritualText;

        GameManager manager;
        public float dropDistance;

        private void Awake()
        {
            manager = FindObjectOfType<GameManager>();
        }
        public bool activeContent()
        {
            if (!gameObject.activeInHierarchy || placeInQueue == 0/* || placeInQueue == null*/)
                return false;
            else
                return true;
        }

        private void OnEnable()
        {
            chosen = false;
            titleText.text = content.title;
            descriptionText.text = content.description;
            graphic.sprite = content.graphic;
            physicalText.text = $"{content.physical}";
            mentalText.text = $"{content.mental}";
            spiritualText.text = $"{content.spiritual}";
        }

        public void CheckIfPicked()
        {
            if (placeInQueue != 0)
            {
                Timing.RunCoroutine(_Drop(dropDistance, manager.buildUp), Segment.Update);
                return;
            }
            chosen = true;
            Picked();
        }

        public void CheckIfDespawned()
        {
            if (placeInQueue != 0)
            {
                Timing.RunCoroutine(_Drop(dropDistance, manager.buildUp), Segment.Update);
                return;
            }


            Timing.RunCoroutine(_Drop(dropDistance + 100, manager.buildUp), Segment.Update);

            //Despawn();
        }

        void Picked()
        {
            //GameManager manager = FindObjectOfType<GameManager>();
            manager.UpdateStats(content.physical, content.mental, content.spiritual);
            Timing.RunCoroutine(_Shared(manager.GetComponentInChildren<SharedFeed>().gameObject.GetComponent<RectTransform>(), manager.buildUp), Segment.Update);
            //Despawn();
        }

        IEnumerator<float> _Drop(float distance, float time)
        {
            float timer = 0;
            Vector3 pos = gameObject.transform.localPosition;
            while (timer < time)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                gameObject.transform.localPosition = Vector3.Lerp(pos, new Vector3(pos.x, pos.y - distance, pos.z), timer);
            }

            if (placeInQueue == 0)
                Despawn();
        }

        public void WindowMissed()
        {
            if (!activeContent())
                return;

            placeInQueue -= 1;

            CheckIfDespawned();
        }

        //void Shared(RectTransform position, float time)
        //{
        //    Getc
        //}

        IEnumerator<float> _Shared(RectTransform position, float time)
        {
            Vector3 pos = GetComponent<RectTransform>().position;
            float timer = 0;
            Vector3 newScale = gameObject.transform.localScale;
            while (timer < time)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position.position, timer);
                newScale.x = Mathf.Lerp(1, -1, timer * 5);
                gameObject.transform.localScale = newScale;
                physicalText.gameObject.transform.localScale = newScale;
                mentalText.gameObject.transform.localScale = newScale;
                spiritualText.gameObject.transform.localScale = newScale;
            }
            Despawn();
            //yield return Timing.WaitForSeconds(1);
        }

        void Despawn()
        {
            chosen = false;
            titleText.text = null;
            descriptionText.text = null;
            graphic.sprite = null;
            physicalText.text = null;
            mentalText.text = null;
            spiritualText.text = null;
            //content = null;
            placeInQueue = 0;

            gameObject.transform.localScale = Vector3.one;
            physicalText.gameObject.transform.localScale = Vector3.one;
            mentalText.gameObject.transform.localScale = Vector3.one;
            spiritualText.gameObject.transform.localScale = Vector3.one;


            gameObject.SetActive(false);
        }

        public void LeftPressed()
        {
            if (!activeContent())
                return;

            placeInQueue -= 1;

            if (side != Side.L)
            {
                //Debug.Log("Right Side");
                CheckIfDespawned();
                return;
            }

            CheckIfPicked();
        }
        public void RightPressed()
        {
            if (!activeContent())
                return;

            placeInQueue -= 1;
            
            if (side != Side.R)
            {
                //Debug.Log("Left Side");
                CheckIfDespawned();
                return;
            }

            CheckIfPicked();
        }
    }
}

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
        public MPImage background;
        public TextMeshProUGUI physicalText;
        public TextMeshProUGUI mentalText;
        public TextMeshProUGUI spiritualText;

        Vector3 pStat;
        Vector3 mStat;
        Vector3 sStat;

        GameManager manager;
        public float dropDistance;

        private void Awake()
        {
            manager = FindObjectOfType<GameManager>();
            GetStatSpots();
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
            background.sprite = content.background;
            background.color = content.tint;
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
                Timing.RunCoroutine(_Drop(dropDistance, manager.buildUp / 2), Segment.Update);
                return;
            }


            Timing.RunCoroutine(_Drop(dropDistance + 100, manager.buildUp / 2), Segment.Update);

            //Despawn();
        }

        void Picked()
        {
            //GameManager manager = FindObjectOfType<GameManager>();            
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
                gameObject.transform.localPosition = Vector3.Lerp(pos, new Vector3(pos.x, pos.y - distance, pos.z), timer * 2);
            }

            pos.y -= distance;
            gameObject.transform.localPosition = pos;

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

        void GetStatSpots()
        {
            StatSpot[] spots = FindObjectsOfType<StatSpot>();

            foreach (StatSpot spot in spots)
            {
                if (spot.gameObject.name == "P")
                {
                    pStat = spot.gameObject.transform.position;
                }
                else if (spot.gameObject.name == "M")
                {
                    mStat = spot.gameObject.transform.position;
                }
                else if (spot.gameObject.name == "S")
                {
                    sStat = spot.gameObject.transform.position;
                }
            }
        }

        IEnumerator<float> _Shared(RectTransform position, float time)
        {
            float timer = 0;
            Vector3 newScale = gameObject.transform.localScale;
            while (timer < time / 2)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position.position, timer * 2);
                newScale.x = Mathf.Lerp(1, -1, timer * 3.5f);
                gameObject.transform.localScale = newScale;
                physicalText.gameObject.transform.localScale = newScale;
                mentalText.gameObject.transform.localScale = newScale;
                spiritualText.gameObject.transform.localScale = newScale;
            }

            timer = 0;
            Vector3 pos = GetComponent<RectTransform>().localPosition;
            pos.y += 3 * dropDistance;

            Vector3 ppos = physicalText.gameObject.transform.position;
            Vector3 mpos = mentalText.gameObject.transform.position;
            Vector3 spos = spiritualText.gameObject.transform.position;

            Vector3 ppos2 = physicalText.gameObject.transform.localPosition;
            Vector3 mpos2 = mentalText.gameObject.transform.localPosition;
            Vector3 spos2 = spiritualText.gameObject.transform.localPosition;

            //Vector3 ppos2;
            //Vector3 mpos2;
            //Vector3 spos2;

            //StatSpot[] spots = FindObjectsOfType<StatSpot>();

            //foreach (StatSpot spot in spots)
            //{
            //    if (spot.gameObject.name == "P")
            //    {
            //        ppos2 = spot.gameObject.transform.position;
            //    }
            //    else if (spot.gameObject.name == "M")
            //    {
            //        mpos2 = spot.gameObject.transform.position;
            //    }
            //    else if (spot.gameObject.name == "S")
            //    {
            //        spos2 = spot.gameObject.transform.position;
            //    }
            //}
            //Vector3 ppos2 = FindObjectOfType

            while (timer < time / 2)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, pos, timer);

                physicalText.gameObject.transform.position = Vector3.Lerp(ppos, pStat, timer * 2);
                mentalText.gameObject.transform.position = Vector3.Lerp(mpos, mStat, timer * 2);
                spiritualText.gameObject.transform.position = Vector3.Lerp(spos, sStat, timer * 2);


                float newA = Mathf.Lerp(1, 0, timer * 2);

                

                Color textColor = titleText.color;
                textColor.a = newA;
                titleText.color = textColor;
                descriptionText.color = textColor;

                Color graphicColor = graphic.color;
                graphicColor.a = newA;
                graphic.color = graphicColor;

                Color backgroundColor = background.color;
                backgroundColor.a = newA;
                background.color = backgroundColor;

                //titleText.color.SetAlpha(newA);
                //descriptionText.color.SetAlpha(newA);
                //graphic.color.SetAlpha(newA);
            }
            manager.UpdateStats(content.physical, content.mental, content.spiritual);

            Despawn();
            physicalText.gameObject.transform.localPosition = ppos2;
            mentalText.gameObject.transform.localPosition = mpos2;
            spiritualText.gameObject.transform.localPosition = spos2;
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
            //titleText.color.SetAlpha(1);
            //descriptionText.color.SetAlpha(1);
            //graphic.color.SetAlpha(1);
            //content = null;


            Color textColor = titleText.color;
            textColor.a = 1;
            titleText.color = textColor;
            descriptionText.color = textColor;

            Color graphicColor = graphic.color;
            graphicColor.a = 1;
            graphic.color = graphicColor;

            Color backgroundColor = background.color;
            backgroundColor.a = 1;
            background.color = backgroundColor;

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

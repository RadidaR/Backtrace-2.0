using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HellTap.PoolKit;

namespace MMC
{
    public enum Type { P, S, M, J};
    public enum Side { L, R};
    public class ContentSpawner : MonoBehaviour
    {
        public ContentObject[] allContent;
        public List<ContentObject> contentList;
        public List<ContentObject> currentContent;
        public bool currentSetPositive;
        public int contentSetSize;
        //public Spawner mySpawner;
        public GameObject contentPrefab;
        public GameObject canvas;
        public int poolSize;
        public GameObject[] contentPool;

        public RectTransform[] positions;

        public int poolCounter = 0;




        private void Awake()
        {
            contentPool = new GameObject[poolSize];
            ResetContentList();

            for (int i = 0; i < poolSize; i++)
            {
                contentPool[i] = Instantiate(contentPrefab, canvas.transform);
            }

            foreach (GameObject content in contentPool)
            {
                content.gameObject.SetActive(false);
            }

            FillCurrentContent();
            SetInitialPositions();
        }

        void SetInitialPositions()
        {
            for (int i = 0; i < 8; i++)
            {
                int random = Random.Range(0, currentContent.Count);
                ContentScript script = contentPool[i].GetComponent<ContentScript>();
                script.content = currentContent[random];
                script.placeInQueue = Mathf.RoundToInt((i + 1.05f) / 2);
                if (i == 0 || i == 2 || i == 4 || i == 6)
                {
                    script.side = Side.L;
                }
                else
                {
                    script.side = Side.R;
                }
                currentContent.Remove(currentContent[random]);
                contentPool[i].GetComponent<RectTransform>().position = positions[i].position;
                contentPool[i].SetActive(true);
                poolCounter++;
            }

        }

        public void SpawnNewContent()
        {
            if (currentContent.Count < 4)
            {
                Debug.Log("Refilling");
                FillCurrentContent();
            }                  

            Spawn(Side.L);
            Spawn(Side.R);


            //script.placeInQueue = 

        }

        void Spawn(Side side)
        {
            if (poolCounter == contentPool.Length - 1)
            {
                Debug.Log("Reset Pool");
                poolCounter = 0;
            }

            ContentScript script = contentPool[poolCounter].GetComponent<ContentScript>();
            int random = Random.Range(0, currentContent.Count);

            if (side == Side.L)
            {
                Debug.Log($"LEFT number: {random} ; size: {currentContent.Count}");
                script.content = currentContent[random];
                script.placeInQueue = 4;
                script.side = Side.L;
                currentContent.Remove(currentContent[random]);
                contentPool[poolCounter].GetComponent<RectTransform>().position = positions[6].position;
                contentPool[poolCounter].SetActive(true);
            }
            else if (side == Side.R)
            {
                Debug.Log($"RIGHT number: {random} ; size: {currentContent.Count}");
                script.content = currentContent[random];
                script.placeInQueue = 4;
                script.side = Side.R;
                currentContent.Remove(currentContent[random]);
                contentPool[poolCounter].GetComponent<RectTransform>().position = positions[7].position;
                contentPool[poolCounter].SetActive(true);

            }
            poolCounter++;
        }

        void FillCurrentContent()
        {
            //currentContent.Clear();
            if (contentList.Count <= contentSetSize)
            {
                foreach (ContentObject content in contentList)
                {
                    currentContent.Add(content);
                }
                ResetContentList();
            }
            else
            {
                int random = Random.Range(0, contentList.Count);
                //Debug.Log($"{random}");
                if (contentList[random].type != Type.J)
                {
                    currentContent.Add(contentList[random]);
                    contentList.RemoveAt(random);
                    CompleteSet(currentContent[currentContent.Count - 1]);
                }
                else
                {
                    bool noTypes = true;
                    foreach (ContentObject content in contentList)
                    {
                        if (content.type != Type.J)
                            noTypes = false;
                    }

                    if (!noTypes)
                    {
                        while (contentList[random].type == Type.J)
                        {
                            random = Random.Range(0, contentList.Count);
                            //Debug.Log($"{random}");
                            if (contentList[random].type != Type.J)
                                break;
                        }
                        currentContent.Add(contentList[random]);
                        contentList.RemoveAt(random);
                        CompleteSet(currentContent[currentContent.Count - 1]);
                    }
                    else
                    {
                        ResetContentList();
                        FillCurrentContent();
                        return;
                    }
                }
            }
            SetCurrentValue(currentContent[0]);

            while (currentContent.Count < contentSetSize)
            {
                ContentObject junk = FindPositiveType(Type.J);
                currentContent.Add(junk);
                contentList.Remove(junk);
            }
        }

        void CompleteSet(ContentObject type)
        {
            ContentObject J1 = FindPositiveType(Type.J);
            ContentObject J2 = FindPositiveType(Type.J);
            if (!type.negative)
            {
                ContentObject P = FindPositiveType(Type.P);
                ContentObject M = FindPositiveType(Type.M);
                ContentObject S = FindPositiveType(Type.S);

                if (type.type == Type.P)
                {
                    if (M != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (S != null)
                    {
                        currentContent.Add(S);
                        contentList.Remove(S);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }
                else if (type.type == Type.M)
                {
                    if (P != null)
                    {
                        currentContent.Add(P);
                        contentList.Remove(P);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (S != null)
                    {
                        currentContent.Add(S);
                        contentList.Remove(S);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }
                else if (type.type == Type.S)
                {
                    if (P != null)
                    {
                        currentContent.Add(P);
                        contentList.Remove(P);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (M != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }                
            }
            else
            {
                ContentObject P = FindNegativeType(Type.P);
                ContentObject M = FindNegativeType(Type.M);
                ContentObject S = FindNegativeType(Type.S);

                if (type.type == Type.P)
                {
                    if (M != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (S != null)
                    {
                        currentContent.Add(S);
                        contentList.Remove(S);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }
                else if (type.type == Type.M)
                {
                    if (P != null)
                    {
                        currentContent.Add(P);
                        contentList.Remove(P);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (S != null)
                    {
                        currentContent.Add(S);
                        contentList.Remove(S);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }
                else if (type.type == Type.S)
                {
                    if (P != null)
                    {
                        currentContent.Add(P);
                        contentList.Remove(P);
                    }
                    else
                    {
                        currentContent.Add(J1);
                        contentList.Remove(J1);
                    }

                    if (M != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
                    }
                    else
                    {
                        currentContent.Add(J2);
                        contentList.Remove(J2);
                    }
                }
            }
        }

        void SetCurrentValue (ContentObject content)
        {
            if (content.negative)
            {
                currentSetPositive = false;
            }
            else
            {
                currentSetPositive = true;
            }
        }

        void ResetContentList()
        {
            contentList.Clear();
            foreach (ContentObject content in allContent)
            {
                contentList.Add(content);
            }

        }

        public ContentObject FindPositiveType (Type type)
        {
            ContentObject content = null;
            for (int i = 0; i < contentList.Count; i++)
            {
                if (contentList[i].type == type)
                {
                    if (!contentList[i].negative)
                    {
                        content = contentList[i];
                        break;
                    }
                }

                if (i == contentList.Count - 1)
                {
                    Debug.Log("No content of that type");
                }
            }
            return content;
        }
        public ContentObject FindNegativeType (Type type)
        {
            ContentObject content = null;
            for (int i = 0; i < contentList.Count; i++)
            {
                if (contentList[i].type == type)
                {
                    if (contentList[i].negative)
                    {
                        content = contentList[i];
                        break;
                    }
                }

                if (i == contentList.Count - 1)
                {
                    Debug.Log("No content of that type");
                }
            }
            return content;
        }
    }
}

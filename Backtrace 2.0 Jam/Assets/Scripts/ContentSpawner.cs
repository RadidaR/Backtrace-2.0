using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public int contentSetSize;
        //public Spawner mySpawner;
        public GameObject contentPrefab;
        public GameObject canvas;
        public int poolSize;
        public GameObject[] contentPool;


        private void Awake()
        {
            contentPool = new GameObject[poolSize];
            foreach (ContentObject content in allContent)
            {
                contentList.Add(content);
            }

            for (int i = 0; i < poolSize; i++)
            {
                contentPool[i] = Instantiate(contentPrefab, canvas.transform);
            }

            foreach (GameObject content in contentPool)
            {
                content.gameObject.SetActive(false);
            }

            FillCurrentContent();

        }

        void FillCurrentContent()
        {
            currentContent.Clear();
            if (contentList.Count <= contentSetSize)
            {
                foreach (ContentObject content in contentList)
                {
                    currentContent.Add(content);
                }
                contentList.Clear();
            }
            else
            {
                int random = Random.Range(0, contentList.Count);
                Debug.Log($"{random}");
                if (contentList[random].type != Type.J)
                {
                    currentContent.Add(contentList[random]);
                    contentList.RemoveAt(random);
                    CompleteSet(currentContent[currentContent.Count - 1]);
                }
                else
                {
                    while (contentList[random].type == Type.J)
                    {
                        random = Random.Range(0, contentList.Count);
                        Debug.Log($"{random}");
                        if (contentList[random].type != Type.J)
                            break;
                    }
                    currentContent.Add(contentList[random]);
                    contentList.RemoveAt(random);
                    CompleteSet(currentContent[currentContent.Count - 1]);

                }
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
                else if (type.type == Type.S)
                {
                    if (P != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
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
                else if (type.type == Type.S)
                {
                    if (P != null)
                    {
                        currentContent.Add(M);
                        contentList.Remove(M);
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

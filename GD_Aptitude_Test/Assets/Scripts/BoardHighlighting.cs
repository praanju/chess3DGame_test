using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeSpace.Chess
{
    public class BoardHighlighting : MonoBehaviour
    {
        public static BoardHighlighting Instance { get; set; }

        public GameObject highlightPrefab;
        private List<GameObject> highlights;

        void Start()
        {
            Instance = this;
            highlights = new List<GameObject>();

        }

        private GameObject GetHighlightObject()
        {
            GameObject go = highlights.Find(g => !g.activeSelf);
            if(go == null)
            {
                go = Instantiate(highlightPrefab);
                highlights.Add(go);
            }
            return go;
        }

        public void HighlightAllowedMoves(bool[,] moves)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (moves[i, j])
                    {
                        GameObject go = GetHighlightObject();
                        go.SetActive(true);
                        go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                    }
                }
            }
        }

        public void HideHighlights()
        {
            foreach (GameObject go in highlights) go.SetActive(false);
        }
    }
}


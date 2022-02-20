using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeSpace.Chess
{ 
    public class DiffPlayers : MonoBehaviour
    {
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public bool isWhite;

        public void SetPosition(int x, int y)
        {
            CurrentX = x;
            CurrentY = y;
        }

        public virtual bool[,] PossibleMove()
        {
            return new bool[6, 6];
        }
    }
}



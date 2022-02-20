using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeSpace.Chess
{
    public class King : DiffPlayers
    {
        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[6, 6];

            DiffPlayers c;
            int i, j;

            // Top
            i = CurrentX - 1;
            j = CurrentY + 1;
            if (CurrentY < 7)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (i >= 0 && i < 6)
                    {
                        c = Board.Instance.PiecesPositions[i, j];
                        if (c == null) r[i, j] = true;
                        else if (c.isWhite != isWhite) r[i, j] = true;
                    }
                    i++;
                }
            }

            // Bottom
            i = CurrentX - 1;
            j = CurrentY - 1;
            if (CurrentY > 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (i >= 0 && i < 6)
                    {
                        c = Board.Instance.PiecesPositions[i, j];
                        if (c == null) r[i, j] = true;
                        else if (c.isWhite != isWhite) r[i, j] = true;
                    }
                    i++;
                }
            }

            // Left
            if (CurrentX > 0)
            {
                c = Board.Instance.PiecesPositions[CurrentX - 1, CurrentY];
                if (c == null) r[CurrentX - 1, CurrentY] = true;
                else if (c.isWhite != isWhite) r[CurrentX - 1, CurrentY] = true;
            }

            // Right
            if (CurrentX < 7)
            {
                c = Board.Instance.PiecesPositions[CurrentX + 1, CurrentY];
                if (c == null) r[CurrentX + 1, CurrentY] = true;
                else if (c.isWhite != isWhite) r[CurrentX + 1, CurrentY] = true;
            }

            return r;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeSpace.Chess
{
    public class Pawn : DiffPlayers
    {
        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[6, 6];
            DiffPlayers t, t2;

            if(isWhite)
            {
                //diagonal left
                if(CurrentX != 0 && CurrentY != 5)
                {
                    t = Board.Instance.PiecesPositions[CurrentX - 1, CurrentY - 1];
                    if (t != null && !t.isWhite) r[CurrentX - 1, CurrentY - 1] = true;
                }

                // Diagonal Right
                if (CurrentX != 7 && CurrentY != 7)
                {
                    t = Board.Instance.PiecesPositions[CurrentX + 1, CurrentY + 1];
                    if (t != null && !t.isWhite) r[CurrentX + 1, CurrentY + 1] = true;
                }

                // Forward
                if (CurrentY != 7)
                {
                    t = Board.Instance.PiecesPositions[CurrentX, CurrentY + 1];
                    if (t == null) r[CurrentX, CurrentY + 1] = true;
                }

                // Two Steps Forward
                if (CurrentY == 1)
                {
                    t = Board.Instance.PiecesPositions[CurrentX, CurrentY + 1];
                    t2 = Board.Instance.PiecesPositions[CurrentX, CurrentY + 2];
                    if (t == null && t2 == null) r[CurrentX, CurrentY + 2] = true;
                }
            }

            else
            {
                // Diagonal Left
                if (CurrentX != 0 && CurrentY != 0)
                {
                    t = Board.Instance.PiecesPositions[CurrentX - 1, CurrentY - 1];
                    if (t != null && t.isWhite) r[CurrentX - 1, CurrentY - 1] = true;
                }

                // Diagonal Right
                if (CurrentX != 7 && CurrentY != 0)
                {
                    t = Board.Instance.PiecesPositions[CurrentX + 1, CurrentY - 1];
                    if (t != null && t.isWhite) r[CurrentX + 1, CurrentY - 1] = true;
                }

                // Forward
                if (CurrentY != 0)
                {
                    t = Board.Instance.PiecesPositions[CurrentX, CurrentY - 1];
                    if (t == null) r[CurrentX, CurrentY - 1] = true;
                }

                // Two Steps Forward
                if (CurrentY == 6)
                {
                    t = Board.Instance.PiecesPositions[CurrentX, CurrentY - 1];
                    t2 = Board.Instance.PiecesPositions[CurrentX, CurrentY - 2];
                    if (t == null && t2 == null) r[CurrentX, CurrentY - 2] = true;
                }
            }

            return r;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeSpace.Chess
{
    public class Knight : DiffPlayers
    {
        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[6, 6];

            // Up / Left
            KnightMove(CurrentX - 1, CurrentY + 2, ref r);
            KnightMove(CurrentX - 2, CurrentY + 1, ref r);

            // Up / Right
            KnightMove(CurrentX + 1, CurrentY + 2, ref r);
            KnightMove(CurrentX + 2, CurrentY + 1, ref r);

            // Down / Left
            KnightMove(CurrentX - 1, CurrentY - 2, ref r);
            KnightMove(CurrentX - 2, CurrentY - 1, ref r);

            // Down / Right
            KnightMove(CurrentX + 1, CurrentY - 2, ref r);
            KnightMove(CurrentX + 2, CurrentY - 1, ref r);

            return r;
        }

        public void KnightMove(int x, int y, ref bool[,] r)
        {
            DiffPlayers c;
            if (x >= 0 && x < 6 && y >= 0 && y < 6)
            {
                c = Board.Instance.PiecesPositions[x, y];
                if (c == null) r[x, y] = true;
                else if (c.isWhite != isWhite) r[x, y] = true;
            }
        }
    }
}


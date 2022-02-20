using UnityEngine;

namespace ThreeSpace.Chess
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] public Vector2Int Position;

        public Board board;
        public GameObject thisPiece;
      
        void Start()
        {
            board = GetComponentInParent<Board>();
        }

        void Update()
        {
            if (board.currentTile != null)
            {
                thisPiece = board.currentTile.pieceOnTile;
            }
        }
    }
}

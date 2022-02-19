using UnityEngine;

namespace ThreeSpace.Chess
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] public Vector2Int Position;

        //
        [SerializeField] public GameObject[] pieces;
        private Piece[] actualPieces;
        public GameObject pieceOnTile;
        

        void Start()
        {
            actualPieces = new Piece[18];
        }

        void Update()
        {
            pieces = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < pieces.Length; i++)
            {
                actualPieces[i] = pieces[i].GetComponent<Piece>();
            }

            foreach (Piece piece in actualPieces)
            {
                if (piece.Position == Position)
                {
                    pieceOnTile = piece.gameObject;
                    Debug.Log(pieceOnTile.name + " is on tile " + this.gameObject.name);
                }
            }
        }
    }
}

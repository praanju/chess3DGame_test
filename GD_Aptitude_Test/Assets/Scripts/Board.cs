using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace ThreeSpace.Chess
{
    public class Board : MonoBehaviour
    {
        public delegate void TileEventHandler(Tile tile);

        public event TileEventHandler OnMouseDown;
        public event TileEventHandler OnMouseUp;
        public event TileEventHandler OnMouseHover;

        private Tile[,] _tiles;

        private Tile _lastHovered;
        public Tile currentTile;

        public static Board Instance { get; set; }

        private int selectionX;
        private int selectionY;
        private const float tileSize = 1.0f;
        private const float tileOffset = 0.5f;

        private bool[,] allowedMoves { get; set; }
        public DiffPlayers[,] PiecesPositions { get; set; }
        private DiffPlayers currentPiece;
        public List<GameObject> pieces;
        private List<GameObject> actualpieces = new List<GameObject>();

        public bool isWhiteTurn = true;

        public void Awake()
        {
            _tiles = new Tile[6, 6];

            foreach (var tile in GetComponentsInChildren<Tile>())
            {
                _tiles[tile.Position.x, tile.Position.y] = tile;
            }
            
            Instance = this;

            PiecesPositions = new DiffPlayers[6, 6];

            InPiecesPositions();
        }

        public void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                currentTile = hit.collider.gameObject.GetComponent<Tile>();

                UpdateHoveredTile(currentTile);

                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseDown?.Invoke(currentTile);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnMouseUp?.Invoke(currentTile);
                }
            }
            else
            {
                UpdateHoveredTile(null);
            }

            //DrawSelectionForRaycast();
            UpdateSelection();

            if (Input.GetMouseButtonDown(0))
            {
                if (selectionX >= 0 && selectionY >= 0)
                {
                    if (currentPiece == null)
                    {
                        // Select Figure
                            SelectCurrentPiece(selectionX, selectionY);
                    }
                    else
                    {
                        // Move Figure
                        MoveCurrentPiece(selectionX, selectionY);
                    }
                }
            }


        }

        private void UpdateHoveredTile(Tile tile)
        {
            if (_lastHovered != tile)
            {
                OnMouseHover?.Invoke(tile);
                _lastHovered = tile;
            }
        }

        /*private void DrawSelectionForRaycast()
        {
            Vector3 widthLine = Vector3.right * 6;
            Vector3 heightLine = Vector3.forward * 6;

            // Draw Chessboard
            for (int i = 0; i <= 6; i++)
            {
                Vector3 start = Vector3.forward * i;
                Debug.DrawLine(start, start + widthLine);
                for (int j = 0; j <= 6; j++)
                {
                    start = Vector3.right * j;
                    Debug.DrawLine(start, start + heightLine);
                }
            }

            if (selectionX >= 0 && selectionY >= 0)
            {
                Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                    Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
                Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * (selectionX + 1),
                    Vector3.forward * (selectionY + 1) + Vector3.right * selectionX);
            }
        }*/

        private void UpdateSelection()
        {
            if (!Camera.main) return;
            RaycastHit hit;
            float raycastDistance = 25.0f;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastDistance, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
            Debug.Log("Mouse is on the board.");

        }

        private void piecesPos(int index, int x, int y)
        {
            GameObject go = Instantiate(pieces[index], GetTileCenter(x, y), rotation: pieces[index].transform.rotation);
            go.transform.SetParent(transform);
            PiecesPositions[x, y] = go.GetComponent<DiffPlayers>();
            PiecesPositions[x, y].SetPosition(x, y);
            actualpieces.Add(go);
        }

        private Vector3 GetTileCenter(int x, int y)
        {
            Vector3 origin = Vector3.zero;
            origin.x = (tileSize * x) + tileOffset;
            origin.z += (tileSize * y) + tileOffset;
            return origin;
        }

        private void InPiecesPositions()
        {
            piecesPos(0, 3, 0); //whiteking
            piecesPos(1, 4, 0); //whiteknight
            piecesPos(1, 1, 0); //whiteknight
            piecesPos(2, 5, 1); //whitepawn
            piecesPos(2, 4, 1);
            piecesPos(2, 3, 1);
            piecesPos(2, 2, 1);
            piecesPos(2, 1, 1);
            piecesPos(2, 0, 1);

            piecesPos(3, 2, 5); //blackking
            piecesPos(4, 1, 5); //blackknight
            piecesPos(4, 4, 5); //blackknight
            piecesPos(5, 5, 4); //blackpawn
            piecesPos(5, 4, 4);
            piecesPos(5, 3, 4);
            piecesPos(5, 2, 4);
            piecesPos(5, 1, 4);
            piecesPos(5, 0, 4);

        }

        private void SelectCurrentPiece(int x, int y)
        {
            if (PiecesPositions[x, y] == null) return;
            if (PiecesPositions[x, y].isWhite != isWhiteTurn) return;

            bool hasAtLeastOneMove = false;
            allowedMoves = PiecesPositions[x, y].PossibleMove();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (allowedMoves[i, j])
                    {
                        hasAtLeastOneMove = true;

                        // break outer loop
                        i = 7;

                        // break inner loop
                        break;
                    }
                }
            }

            if (!hasAtLeastOneMove) return;

            currentPiece = PiecesPositions[x, y];

            BoardHighlighting.Instance.HighlightAllowedMoves(allowedMoves);

        }

        private void MoveCurrentPiece(int x, int y)
        {
            if(allowedMoves[x, y])
            {
                DiffPlayers d = PiecesPositions[x, y];
                if(d != null && d.isWhite != isWhiteTurn)
                {
                    actualpieces.Remove(d.gameObject);
                    Destroy(d.gameObject);

                    if(d.GetType() == typeof(King))
                    {
                        EndGame();
                        return;
                    }
                }
                PiecesPositions[currentPiece.CurrentX, currentPiece.CurrentY] = null;
                currentPiece.transform.position = GetTileCenter(x, y);
                currentPiece.SetPosition(x, y);
                PiecesPositions[x, y] = currentPiece;
                isWhiteTurn = !isWhiteTurn;
            }

            BoardHighlighting.Instance.HideHighlights();
            currentPiece = null;
        }

        private void EndGame()
        {
            if (isWhiteTurn)
                Debug.Log("White won!");
            else
                Debug.Log("Black won!");

            foreach (GameObject go in actualpieces)
                Destroy(go);

            isWhiteTurn = true;
            BoardHighlighting.Instance.HideHighlights();
            InPiecesPositions();
            Debug.Log("White:" + isWhiteTurn);

        }        

    }
}

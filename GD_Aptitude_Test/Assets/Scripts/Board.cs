using UnityEngine;

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

        void Awake()
        {
            _tiles = new Tile[6, 6];

            foreach (var tile in GetComponentsInChildren<Tile>())
            {
                _tiles[tile.Position.x, tile.Position.y] = tile;
            }
        }

        void Update()
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
        }

        private void UpdateHoveredTile(Tile tile)
        {
            if (_lastHovered != tile)
            {
                OnMouseHover?.Invoke(tile);
                _lastHovered = tile;
            }
        }
    }
}

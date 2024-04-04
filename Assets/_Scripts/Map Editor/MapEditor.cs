using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.Utilities.Input;

namespace Utilities.MapEditor
{
    public class MapEditor : MonoBehaviour
    {
        [SerializeField] private TilemapCategory[] _tilemaps;

        [SerializeField] private MapTile _buildingTile;

        private Dictionary<TileCategory, Tilemap> _cachedTilemap = new Dictionary<TileCategory, Tilemap>();

        private void Awake()
        {
            foreach (TilemapCategory tilemap in _tilemaps)
                _cachedTilemap.Add(tilemap.category, tilemap.tilemap);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int pos = new Vector3Int((int)MouseInput.MouseWorldPos.x, (int)MouseInput.MouseWorldPos.y);
                _cachedTilemap[_buildingTile.category].SetTile(pos, _buildingTile.tileBase);
            }
        }
    }
}

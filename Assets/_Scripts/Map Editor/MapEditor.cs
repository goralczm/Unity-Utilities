using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.MapEditor.Tiles;
using Utilities.Utilities.Core;

namespace Utilities.MapEditor
{
    public class MapEditor : Singleton<MapEditor>
    {
        [Header("Settings")]
        [SerializeField] private TileCategory _currentEraserTilemap;

        [Header("Instances")]
        [SerializeField] private TilemapCategory[] _tilemaps;
        [SerializeField] private BuildingSystem.BuildingSystem _buildingSystem;

        private Dictionary<TileCategory, Tilemap> _cachedTilemap = new Dictionary<TileCategory, Tilemap>();

        public BuildingSystem.BuildingSystem GetBuildingSystem() => _buildingSystem;

        public void SetEraserTilemap(TileCategory tilemapCategory) => _currentEraserTilemap = tilemapCategory;

        protected override void Awake()
        {
            base.Awake();

            foreach (TilemapCategory tilemap in _tilemaps)
                _cachedTilemap.Add(tilemap.category, tilemap.tilemap);
        }

        public void PlaceTile(TileBrush tile, Vector2 position)
        {
            Vector3Int pos = _cachedTilemap[tile.category].WorldToCell(position);
            _cachedTilemap[tile.category].SetTile(pos, tile.tileBase);

            _currentEraserTilemap = tile.category;
        }

        public void EraseTile(Vector2 position)
        {
            Vector3Int pos = _cachedTilemap[_currentEraserTilemap].WorldToCell(position);
            _cachedTilemap[_currentEraserTilemap].SetTile(pos, null);
        }
    }
}

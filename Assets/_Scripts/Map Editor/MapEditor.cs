using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.MapEditor.Tiles;
using Utilities.Utilities.Core;
using Utilities.Utilities.Input;

namespace Utilities.MapEditor
{
    public class MapEditor : Singleton<MapEditor>
    {
        [Header("Settings")]
        [SerializeField] private TilemapLayer _currentEraserTilemap;

        [Header("Instances")]
        [SerializeField] private TilemapCategory[] _tilemaps;
        [SerializeField] private BuildingSystem.BuildingSystem _buildingSystem;
        [SerializeField] private GameObject _cellPreview;
        [SerializeField] private GameObject _gridPreview;

        private Dictionary<TilemapLayer, Tilemap> _cachedTilemap = new Dictionary<TilemapLayer, Tilemap>();

        public BuildingSystem.BuildingSystem GetBuildingSystem() => _buildingSystem;

        public void SetEraserTilemap(TilemapLayer tilemapCategory) => _currentEraserTilemap = tilemapCategory;

        protected override void Awake()
        {
            base.Awake();

            foreach (TilemapCategory tilemap in _tilemaps)
                _cachedTilemap.Add(tilemap.category, tilemap.tilemap);

            _buildingSystem.BuildingStateChanged += BuildingStateChanged;
        }

        private void BuildingStateChanged(bool isBuilding)
        {
            _cellPreview.transform.position = MouseInput.MouseWorldPos;
            _cellPreview.SetActive(!isBuilding);
        }

        public void PlaceTile(TileBrush tile, Vector2 position)
        {
            Vector3Int pos = _cachedTilemap[tile.category].WorldToCell(position);
            _cachedTilemap[tile.category].SetTile(pos, tile.tileBase);
        }

        public void EraseTile(Vector2 position)
        {
            Vector3Int pos = _cachedTilemap[_currentEraserTilemap].WorldToCell(position);
            _cachedTilemap[_currentEraserTilemap].SetTile(pos, null);
        }

        public void ToggleGridPreview()
        {
            _gridPreview.SetActive(!_gridPreview.activeSelf);
        }
    }
}

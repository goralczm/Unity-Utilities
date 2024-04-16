using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.MapEditor.Tiles;
using Utilities.Utilities;
using Utilities.Utilities.Core;
using Utilities.Utilities.Input;

namespace Utilities.MapEditor
{
    public enum ToolType
    {
        Single_Brush,
        Box_Brush,
        Selection,
    }

    public class MapEditor : Singleton<MapEditor>
    {
        [Header("Settings")]
        [SerializeField] private ToolType _currentTool;

        [Header("Instances")]
        [SerializeField] private List<TilemapCategory> _tilemaps;
        [SerializeField] private BuildingSystem.BuildingSystem _buildingSystem;
        [SerializeField] private Tilemap _previewTilemap;
        [SerializeField] private GameObject _gridPreview;

        private TileBrush _brush;
        private RectangleSelection _selection;
        private bool _isPreview;

        private Dictionary<TilemapLayer, Tilemap> _cachedTilemap = new Dictionary<TilemapLayer, Tilemap>();

        public Action<ToolType> OnToolChanged;

        public BuildingSystem.BuildingSystem GetBuildingSystem()
        {
            return _buildingSystem;
        }

        public void SetCurrentTool(int toolIndex)
        {
            CancelPreview();
            _currentTool = (ToolType)toolIndex;

            OnToolChanged?.Invoke(_currentTool);
        }

        protected override void Awake()
        {
            base.Awake();

            foreach (TilemapCategory tilemap in _tilemaps)
                _cachedTilemap.Add(tilemap.category, tilemap.tilemap);
        }

        private void Update()
        {
            switch (_currentTool)
            {
                case ToolType.Single_Brush:
                    if (Input.GetMouseButtonDown(1))
                    {
                        CancelPreview();
                        _brush = null;
                        SetCurrentTool((int)ToolType.Selection);
                    }

                    if (_brush != null)
                    {
                        BeginPreview(_brush);
                        PlaceTile(_brush, MouseInput.MouseWorldPos);

                        if (Input.GetMouseButton(0) && !Helpers.IsMouseOverUI())
                            ApplyPreview(_brush);
                    }

                    break;
                case ToolType.Box_Brush:
                    if (Input.GetMouseButtonDown(1))
                    {
                        CancelPreview();

                        if (_selection == null)
                        {
                            _brush = null;
                            SetCurrentTool((int)ToolType.Selection);
                        }
                        else
                        {
                            _selection.Dispose();
                            _selection = null;
                        }
                    }

                    if (_brush != null)
                    {
                        if (Input.GetMouseButtonDown(0) && !Helpers.IsMouseOverUI())
                        {
                            _selection = new RectangleSelection();
                            _selection.BeginSelection();
                        }

                        if (_selection != null)
                        {
                            if (Input.GetMouseButton(0))
                            {
                                _selection.OnMouseHold();
                                BeginPreview(_brush);
                                PlaceTileInBounds(_brush, _selection.GetRect().GetBounds());
                            }

                            if (Input.GetMouseButtonUp(0))
                            {
                                ApplyPreview(_brush);
                                _selection.Dispose();
                                _selection = null;
                            }
                        }
                        else
                        {
                            BeginPreview(_brush);
                            PlaceTile(_brush, MouseInput.MouseWorldPos);
                        }
                    }

                    break;
            }
        }

        public void BeginPaint(TileBrush brush)
        {
            _brush = brush;
            if (_currentTool == ToolType.Selection)
                SetCurrentTool((int)ToolType.Single_Brush);
        }

        public void BeginPreview(TileBrush tile)
        {
            _isPreview = true;
            _previewTilemap.ClearAllTiles();
            foreach (var tilemap in _cachedTilemap.Values)
                tilemap.gameObject.SetActive(true);

            TileBase[] tiles = _cachedTilemap[tile.category].GetTilesBlock(_cachedTilemap[tile.category].cellBounds);
            _previewTilemap.SetTilesBlock(_cachedTilemap[tile.category].cellBounds, tiles);
            _previewTilemap.GetComponent<TilemapRenderer>().sortingOrder = _cachedTilemap[tile.category].GetComponent<TilemapRenderer>().sortingOrder;
            _cachedTilemap[tile.category].gameObject.SetActive(false);
        }

        public void CancelPreview()
        {
            _previewTilemap.ClearAllTiles();
            foreach (var tilemap in _cachedTilemap.Values)
                tilemap.gameObject.SetActive(true);

            _isPreview = false;
        }

        public void ApplyPreview(TileBrush tile)
        {
            if (!_isPreview)
                return;

            _cachedTilemap[tile.category].ClearAllTiles();
            TileBase[] tiles = _previewTilemap.GetTilesBlock(_previewTilemap.cellBounds);
            _cachedTilemap[tile.category].SetTilesBlock(_previewTilemap.cellBounds, tiles);
            _cachedTilemap[tile.category].gameObject.SetActive(true);

            _previewTilemap.ClearAllTiles();

            _isPreview = false;
        }

        public void PlaceTileInBounds(TileBrush tile, Bounds bounds)
        {
            Vector3Int leftBottomCorner = _previewTilemap.WorldToCell(bounds.center - bounds.size / 2f);
            Vector3Int rightTopCorner = _previewTilemap.WorldToCell(bounds.center + bounds.size / 2f);

            var xDir = leftBottomCorner.x < rightTopCorner.x ? 1 : -1;
            var yDir = leftBottomCorner.y < rightTopCorner.y ? 1 : -1;
            int xCols = 1 + Mathf.Abs(leftBottomCorner.x - rightTopCorner.x);
            int yCols = 1 + Mathf.Abs(leftBottomCorner.y - rightTopCorner.y);
            for (var x = 0; x < xCols; x++)
            {
                for (var y = 0; y < yCols; y++)
                {
                    var tilePos = leftBottomCorner + new Vector3Int(x * xDir, y * yDir, 0);
                    _previewTilemap.SetTile(tilePos, tile.tileBase);
                }
            }
        }

        public void PlaceTile(TileBrush tile, Vector2 position)
        {
            Vector3Int pos = _previewTilemap.WorldToCell(position);
            _previewTilemap.SetTile(pos, tile.tileBase);
        }

        public void EraseTile(Vector2 position)
        {
            Vector3Int pos = _previewTilemap.WorldToCell(position);
            _previewTilemap.SetTile(pos, null);
        }

        public void ToggleGridPreview()
        {
            _gridPreview.SetActive(!_gridPreview.activeSelf);
        }

        public void AddTilemap(Tilemap tilemap, TilemapLayer layer)
        {
            _tilemaps.Add(new TilemapCategory { tilemap = tilemap, category = layer });
        }
    }
}

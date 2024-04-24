using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

namespace Utilities.LevelEditor.Commands
{
    public class PlaceTileCommand : CommandPattern.Command
    {
        private readonly Tilemap _originalTilemap;
        private readonly Tilemap _previewTilemap;

        private readonly BoundsInt _originalBounds;
        private readonly BoundsInt _newBounds;

        private readonly TileBase[] _originalTiles;
        private readonly TileBase[] _newTiles;

        public PlaceTileCommand(Tilemap originalTilemap, Tilemap previewTilemap)
        {
            _originalTilemap = originalTilemap;
            _previewTilemap = previewTilemap;

            _originalBounds = _originalTilemap.cellBounds;
            _originalTiles = _originalTilemap.GetTilesBlock(_originalBounds);

            _newBounds = _previewTilemap.cellBounds;
            _newTiles = _previewTilemap.GetTilesBlock(_newBounds);
        }

        public override bool IsFinished => true;

        public override void Execute()
        {
            _originalTilemap.ClearAllTiles();
            _originalTilemap.SetTilesBlock(_newBounds, _newTiles);
            _originalTilemap.gameObject.SetActive(true);
            _previewTilemap.ClearAllTiles();
        }

        public override void Tick()
        {

        }

        public override void Undo()
        {
            _originalTilemap.ClearAllTiles();
            _originalTilemap.SetTilesBlock(_originalBounds, _originalTiles);
        }
    }
}

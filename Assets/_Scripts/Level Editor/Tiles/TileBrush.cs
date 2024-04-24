using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using Utilities.BuildingSystem;

namespace Utilities.LevelEditor.Tiles
{
    [CreateAssetMenu(menuName = "Map Editor/New Tile", fileName = "New Tile")]
    public class TileBrush : BuildingSO
    {
        public TilemapLayer category;
        public TileBase tileBase;

        public Sprite sprite;

        public override Vector2 GetSize()
        {
            return Vector2.one;
        }

        public override Sprite GetSprite()
        {
            return sprite;
        }

        public override void Build(Vector2 position, Quaternion rotation)
        {
            LevelEditor.Instance.PlaceTile(this, position);
        }
    }
}

using UnityEngine;

namespace Utilities.LevelEditor.Tiles
{
    [CreateAssetMenu(menuName = "Map Editor/New Eraser", fileName = "New Eraser")]
    public class TileEraser : TileBrush
    {
        public override void Build(Vector2 position, Quaternion rotation)
        {
            LevelEditor.Instance.EraseTile(position);
        }
    }
}
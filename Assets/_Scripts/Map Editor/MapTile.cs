using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utilities.MapEditor
{
    [CreateAssetMenu(menuName = "Map Editor/New Tile", fileName = "New Tile")]
    public class MapTile : ScriptableObject
    {
        public TileCategory category;
        public TileBase tileBase;
    }
}

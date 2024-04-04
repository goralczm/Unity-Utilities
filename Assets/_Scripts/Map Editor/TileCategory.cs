using UnityEngine.Tilemaps;

namespace Utilities.MapEditor
{
    [System.Serializable]
    public struct TilemapCategory
    {
        public TileCategory category;
        public Tilemap tilemap;
    }

    public enum TileCategory
    {
        Wall,
        Nodes,
        Path,
    }
}

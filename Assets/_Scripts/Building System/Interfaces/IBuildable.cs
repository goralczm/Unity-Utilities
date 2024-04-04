using UnityEngine;

namespace Utilities.BuildingSystem
{
    public interface IBuildable
    {
        public Sprite GetSprite();
        public Vector2 GetSize();
        public void Build(Vector2 position, Quaternion rotation);
    }
}
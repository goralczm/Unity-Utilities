using UnityEngine;

namespace Utilities.BuildingSystem
{
    public abstract class BuildingSO : ScriptableObject, IBuildable
    {
        public abstract Vector2 GetSize();
        public abstract Sprite GetSprite();
        public abstract void Build(Vector2 position, Quaternion rotation);
    }
}

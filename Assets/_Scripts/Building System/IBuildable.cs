using UnityEngine;

namespace Utilities.BuildingSystem
{
    public interface IBuildable
    {
        public Sprite GetSprite();
        public Vector2 GetScale();
        public void Build();
    }
}
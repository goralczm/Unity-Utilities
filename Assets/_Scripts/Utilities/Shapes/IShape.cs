using UnityEngine;

namespace Utilities.Utilities.Shapes
{
    public interface IShape
    {
        public Vector2 GetRandomPositionInside();
        public Vector2 ClampPositionInside(Vector2 position);
        public void OnDrawGizmosSelected();
    }
}

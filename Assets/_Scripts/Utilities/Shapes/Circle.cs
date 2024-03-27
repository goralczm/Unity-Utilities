using UnityEngine;

namespace Utilities.Utilities.Shapes
{
    /// <summary>
    /// Provides functionality for working with a circle.
    /// </summary>
    public class Circle : MonoBehaviour, IShape
    {
        [SerializeField] private float _radius;

        /// <summary>
        /// Generates random point inside a circle.
        /// </summary>
        /// <returns>The generated point as a <see cref="Vector2"/>.</returns>
        public Vector2 GetRandomPositionInside()
        {
            return Random.insideUnitCircle * _radius;
        }

        /// <summary>
        /// Clamps given position inside a circle.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The clamped position as a <see cref="Vector2"/>.</returns>
        public Vector2 ClampPositionInside(Vector2 position)
        {
            Vector2 dir = position - (Vector2)transform.position;

            return Vector2.ClampMagnitude(dir, _radius);
        }

        /// <summary>
        /// Displays the circle as a Gizmo when selected.
        /// </summary>
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}

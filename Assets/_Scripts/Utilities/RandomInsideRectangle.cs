using UnityEngine;

namespace Utilities.Utilities
{
    /// <summary>
    /// Provides functionality of generating random point inside a specified rectangle.
    /// </summary>
    public class RandomInsideRectangle : MonoBehaviour
    {
        [SerializeField] private float _width, _height;

        /// <summary>
        /// Generates random point inside a rectangle.
        /// </summary>
        /// <returns>The generated point as a <see cref="Vector2"/>.</returns>
        public Vector2 GetRandomPositionInside()
        {
            Vector2 bottomLeftCorner = GetBottomLeftCorner();
            Vector2 upperRightCorner = GetUpperRightCorner();

            return new Vector2(Random.Range(bottomLeftCorner.x, upperRightCorner.x),
                               Random.Range(bottomLeftCorner.y, upperRightCorner.y));
        }

        /// <summary>
        /// Retrieves the position of the bottom left corner of the rectangle.
        /// </summary>
        /// <returns>The corner position as a <see cref="Vector2"/>.</returns>
        private Vector2 GetBottomLeftCorner()
        {
            return new Vector2(transform.position.x - _width / 2f, transform.position.y - _height / 2f);
        }

        /// <summary>
        /// Retrieves the position of the upper right corner of the rectangle.
        /// </summary>
        /// <returns>The corner position as a <see cref="Vector2"/>.</returns>
        private Vector2 GetUpperRightCorner()
        {
            return new Vector2(transform.position.x + _width / 2f, transform.position.y + _height / 2f);
        }

        /// <summary>
        /// Displays the rectangle as a Gizmo when selected.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            Vector2 bottomLeftCorner = GetBottomLeftCorner();
            Gizmos.DrawLine(bottomLeftCorner, bottomLeftCorner + new Vector2(_width, 0));
            Gizmos.DrawLine(bottomLeftCorner, bottomLeftCorner + new Vector2(0, _height));

            Vector2 upperRightCorner = GetUpperRightCorner();
            Gizmos.DrawLine(upperRightCorner, upperRightCorner - new Vector2(_width, 0));
            Gizmos.DrawLine(upperRightCorner, upperRightCorner - new Vector2(0, _height));
        }
    }
}

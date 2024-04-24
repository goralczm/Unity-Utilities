using UnityEngine;

namespace Utilities.Utilities.Shapes
{
    /// <summary>
    /// Provides functionality for working with a rectangle.
    /// </summary>
    public class Rectangle : MonoBehaviour, IShape
    {
        [SerializeField] private float _width, _height;

        public float GetWidth() => _width;

        public float GetHeight() => _height;

        public void SetWidth(float width) => _width = width;

        public void SetHeight(float height) => _height = height;

        public bool IsInside(Vector2 position)
        {
            return GetBounds().Contains(position);
        }

        public Bounds GetBounds()
        {
            Bounds bounds = new Bounds();
            bounds.size = new Vector2(_width, _height);
            bounds.center = transform.position;

            return bounds;
        }    

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
        /// Clamps given position inside a rectangle.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The clamped position as a <see cref="Vector2"/>.</returns>
        public Vector2 ClampPositionInside(Vector2 position)
        {
            Vector2 bottomLeftCorner = GetBottomLeftCorner();
            Vector2 upperRightCorner = GetUpperRightCorner();

            return new Vector2(Mathf.Clamp(position.x, bottomLeftCorner.x, upperRightCorner.x),
                               Mathf.Clamp(position.y, bottomLeftCorner.y, upperRightCorner.y));
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
        public void OnDrawGizmosSelected()
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

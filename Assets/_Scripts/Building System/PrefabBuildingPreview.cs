using UnityEngine;

namespace Utilities.BuildingSystem
{
    /// <summary>
    /// Handles the phantom visuals of the currently building prefab in <see cref="BuildingSystem"/>.
    /// </summary>
    public class PrefabBuildingPreview : MonoBehaviour, IBuildingPreview
    {
        [Header("Settings")]
        [SerializeField] private LayerMask _obstaclesLayer;

        [Header("Instances")]
        [SerializeField] private SpriteRenderer _rend;

        private IBuildable _buildable;
        private Collider2D _collider;
        private Vector2 _lastPos;
        private bool _canBuild;
        private float _buildTime;

        /// <summary>
        /// Prepares the phantom building with a sprite, correct collider and scale.
        /// </summary>
        /// <param name="building"></param>
        public void Setup(IBuildable building)
        {
            _buildable = building;
            transform.localScale = building.GetSize();
            _rend.sprite = _buildable.GetSprite();
            _collider = gameObject.AddComponent<PolygonCollider2D>();
            _buildTime = Time.time;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
                RotateLeft();

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                RotateRight();

            if (_canBuild)
                _rend.color = DimOpacity(Color.green, .7f);
            else
                _rend.color = DimOpacity(Color.red, .7f);
        }

        /// <summary>
        /// Sets the opacity of given color to a given intensity value.
        /// </summary>
        /// <param name="color">THe color to be dimmed.</param>
        /// <param name="intensity">The intensity of opacity.</param>
        /// <returns>The dimmed <see cref="Color"/>.</returns>
        private Color DimOpacity(Color color, float intensity)
        {
            color.a = intensity;
            return color;
        }

        /// <summary>
        /// Sets the new position of phantom considering the collision with obstacles.
        /// </summary>
        /// <param name="newPosition">The new position of phantom.</param>
        public void ChangePositionConsideringCollision(Vector2 newPosition)
        {
            if (_lastPos == newPosition)
            {
                _canBuild = !CollidesWithObstacles();
                return;
            }

            ForceChangePosition(newPosition);
        }

        /// <summary>
        /// Sets the position of phantom.
        /// </summary>
        /// <param name="newPosition">The new position of phantom.</param>
        public void ForceChangePosition(Vector2 newPosition)
        {
            transform.position = newPosition;
            _lastPos = transform.position;
        }

        /// <summary>
        /// Checks if the phantom collides with any of the objects on the <see cref="_obstaclesLayer"/> layer.
        /// </summary>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool CollidesWithObstacles()
        {
            if (_collider == null)
                return false;

            return _collider.IsTouchingLayers(_obstaclesLayer);
        }

        /// <summary>
        /// Rotates the phantom -90 degrees.
        /// </summary>
        public void RotateRight()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z - 90f);
        }

        /// <summary>
        /// Rotates the phantom 90 degrees.
        /// </summary>
        public void RotateLeft()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z + 90f);
        }

        /// <summary>
        /// Finalizes the building process and instantiates the building prefab.
        /// </summary>
        public void Build()
        {
            if (!_canBuild || Time.time - _buildTime <= .1f)
                return;

            _buildable.Build(transform.position, transform.rotation);
        }

        public void SetPosition(Vector2 position)
        {
            ChangePositionConsideringCollision(position);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

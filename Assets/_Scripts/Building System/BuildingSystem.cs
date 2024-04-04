using UnityEngine;
using Utilities.Utilities.Input;

namespace Utilities.BuildingSystem
{
    /// <summary>
    /// Handles logic of interactive, grid-based placement of prefabs.
    /// </summary>
    public class BuildingSystem : MonoBehaviour
    {
        [Header("Settings")]
        public bool useGrid;
        [SerializeField] private bool _centerOnGrid;

        [Header("Instances")]
        [SerializeField] private Grid _grid;
        [SerializeField] private BuildingGhost _ghostPrefab;

        private BuildingGhost _currentBuilding;

        private void Update()
        {
            if (_currentBuilding == null)
                return;

            Vector2 cellPos = GetCellPos();
            _currentBuilding.ChangePositionConsideringCollision(cellPos);

            if (UnityEngine.Input.GetMouseButtonDown(0))
                _currentBuilding.Build();
        }

        /// <summary>
        /// Calculates the grid cell position from the current mouse position.
        /// </summary>
        /// <returns>The cell position as <see cref="Vector2"/>.</returns>
        private Vector2 GetCellPos()
        {
            Vector3 mouseWorldPos = MouseInput.MouseWorldPos;
            if (!useGrid)
                return mouseWorldPos;

            Vector3Int cellPos = _grid.WorldToCell(mouseWorldPos);

            if (!_centerOnGrid)
                return new Vector2(cellPos.x, cellPos.y);

            return _grid.GetCellCenterWorld(cellPos);
        }

        /// <summary>
        /// Begins the building process of given prefab.
        /// </summary>
        /// <param name="prefab">The prefab to be built.</param>
        public void StartBuilding(IBuildable prefab)
        {
            CancelBuilding();

            _currentBuilding = Instantiate(_ghostPrefab, Vector2.zero, Quaternion.identity);
            _currentBuilding.Setup(prefab);
        }

        /// <summary>
        /// Cancels current building process.
        /// </summary>
        public void CancelBuilding()
        {
            if (_currentBuilding == null)
                return;

            Destroy(_currentBuilding.gameObject);
        }

        /// <summary>
        /// Rotates the current building -90 degrees.
        /// </summary>
        public void RotateRight()
        {
            _currentBuilding?.RotateLeft();
        }

        /// <summary>
        /// Rotates the current building 90 degrees.
        /// </summary>
        public void RotateLeft()
        {
            _currentBuilding?.RotateRight();
        }
    }
}

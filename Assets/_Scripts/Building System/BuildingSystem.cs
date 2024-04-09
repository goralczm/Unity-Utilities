using System;
using UnityEngine;
using Utilities.Utilities;
using Utilities.Utilities.Input;

namespace Utilities.BuildingSystem
{
    /// <summary>
    /// Handles logic of interactive, grid-based placement of prefabs.
    /// </summary>
    public class BuildingSystem : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _useGrid;
        [SerializeField] private bool _centerOnGrid;

        [Header("Accessibility")]
        [SerializeField] private bool _chainBuilding;

        [Header("Instances")]
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _ghostPrefab;

        private IBuildingPreview _currentGhost;

        public Action<bool> BuildingStateChanged;

        public void SetUseGrid(bool useGrid) => _useGrid = useGrid;

        private void Awake()
        {
            _currentGhost = _ghostPrefab.GetComponent<IBuildingPreview>();
        }

        private void Update()
        {
            if (!_ghostPrefab.activeSelf)
                return;

            Vector2 cellPos = GetCellPosUnderMouse();
            _currentGhost.SetPosition(cellPos);

            if (UnityEngine.Input.GetMouseButtonDown(1))
                CancelBuilding();

            if (Helpers.IsMouseOverUI())
                return;

            if (_chainBuilding)
            {
                if (UnityEngine.Input.GetMouseButton(0))
                    FinishBuilding();
            }
            else
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    FinishBuilding();
            }
        }

        /// <summary>
        /// Calculates the grid cell position from the current mouse position.
        /// </summary>
        /// <returns>The cell position as <see cref="Vector2"/>.</returns>
        public Vector2 GetCellPosUnderMouse()
        {
            Vector3 mouseWorldPos = MouseInput.MouseWorldPos;
            if (!_useGrid)
                return mouseWorldPos;

            Vector3Int cellPos = _grid.WorldToCell(mouseWorldPos);

            if (!_centerOnGrid)
                return new Vector2(cellPos.x, cellPos.y);

            return _grid.GetCellCenterWorld(cellPos);
        }

        /// <summary>
        /// Begins the building process of given prefab.
        /// </summary>
        /// <param name="building">The prefab to be built.</param>
        public void StartBuilding(IBuildable building)
        {
            CancelBuilding();

            _currentGhost.Show();
            _currentGhost.Setup(building);

            BuildingStateChanged?.Invoke(true);
        }

        public void FinishBuilding()
        {
            _currentGhost.Build();

            BuildingStateChanged?.Invoke(false);
        }

        /// <summary>
        /// Cancels current building process.
        /// </summary>
        public void CancelBuilding()
        {
            if (_currentGhost == null)
                return;

            _currentGhost.Hide();
            BuildingStateChanged?.Invoke(false);
        }
    }
}

using UnityEngine;

namespace Utilities.BuildingSystem.Input
{
    /// <summary>
    /// Switches <see cref="BuildingSystem._useGrid"/> state based on player input.
    /// </summary>
    [RequireComponent(typeof(BuildingSystem))]
    public class FreeBuildingWithSnappingInput : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private KeyCode _actionButtton = KeyCode.LeftControl;

        [SerializeField] private bool _invert;

        private BuildingSystem _buildingSystem;

        private void Start()
        {
            _buildingSystem = GetComponent<BuildingSystem>();
        }

        private void Update()
        {
            bool useGrid = UnityEngine.Input.GetKey(_actionButtton);
            if (_invert)
                useGrid = !useGrid;

            _buildingSystem.SetUseGrid(useGrid);
        }
    }
}

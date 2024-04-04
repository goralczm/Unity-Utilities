using System.Collections.Generic;
using UnityEngine;
using Utilities.Utilities.Input;

namespace Utilities.BuildingSystem.Input
{
    /// <summary>
    /// Handles the players input regarding <see cref="BuildingSystem"/>.
    /// </summary>
    [RequireComponent(typeof(BuildingSystem))]
    public class BuildingSystemInput : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<BuildingInput> _buildingInputs = new List<BuildingInput>();

        private BuildingSystem _buildingSystem;

        private void Start()
        {
            _buildingSystem = GetComponent<BuildingSystem>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetMouseButtonDown(1))
                _buildingSystem.CancelBuilding();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
            {
                _buildingSystem.CancelBuilding();
                RaycastHit2D hit = Physics2D.Raycast(MouseInput.MouseWorldPos, Vector2.zero);
                if (hit.collider == null)
                    return;

                Destroy(hit.collider.gameObject);
            }

            foreach (var input in _buildingInputs)
            {
                if (UnityEngine.Input.GetKeyDown(input.key))
                    _buildingSystem.StartBuilding(input.building);
            }
        }
    }
}

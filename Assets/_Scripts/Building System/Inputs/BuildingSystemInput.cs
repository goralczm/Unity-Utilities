using UnityEngine;

[RequireComponent(typeof(BuildingSystem))]
public class BuildingSystemInput : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private BuildingInput[] _buildingInputs;

    private BuildingSystem _buildingSystem;

    private void Start()
    {
        _buildingSystem = GetComponent<BuildingSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            _buildingSystem.CancelBuilding();

        if (Input.GetKeyDown(KeyCode.Q))
            _buildingSystem.RotateLeft();

        if (Input.GetKeyDown(KeyCode.R))
            _buildingSystem.RotateRight();

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            _buildingSystem.CancelBuilding();
            RaycastHit2D hit = Physics2D.Raycast(MouseInput.MouseWorldPos, Vector2.zero);
            if (hit.collider == null)
                return;

            Destroy(hit.collider.gameObject);
        }

        foreach (var input in _buildingInputs)
        {
            if (Input.GetKeyDown(input.key))
                _buildingSystem.StartBuilding(input.building);
        }
    }
}

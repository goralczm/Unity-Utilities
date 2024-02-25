using UnityEngine;
using Utilities.Utilities.Input;

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
        _currentBuilding.ChangePosition(cellPos);

        if (Input.GetMouseButtonDown(0))
            _currentBuilding.Build();
    }

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

    public void StartBuilding(GameObject prefab)
    {
        CancelBuilding();

        _currentBuilding = Instantiate(_ghostPrefab, Vector2.zero, Quaternion.identity);
        _currentBuilding.Setup(prefab);
    }

    public void CancelBuilding()
    {
        if (_currentBuilding == null)
            return;

        Destroy(_currentBuilding.gameObject);
    }

    public void RotateRight()
    {
        _currentBuilding?.RotateLeft();
    }

    public void RotateLeft()
    {
        _currentBuilding?.RotateRight();
    }
}

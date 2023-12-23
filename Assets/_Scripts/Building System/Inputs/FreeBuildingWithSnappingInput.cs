using UnityEngine;

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
        bool useGrid = Input.GetKey(_actionButtton);
        if (_invert)
            useGrid = !useGrid;

        _buildingSystem.useGrid = useGrid;
    }
}

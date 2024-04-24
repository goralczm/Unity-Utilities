using UnityEngine;

namespace Utilities.LevelEditor
{
    public class GridCellPreview : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _followSpeed;

        [Header("Instances")]
        [SerializeField] private BuildingSystem.BuildingSystem _buildingSystem;

        private void Update()
        {
            Vector2 cellPos = _buildingSystem.GetCellPosUnderMouse();
            transform.position = Vector2.Lerp(transform.position, cellPos, Time.deltaTime * _followSpeed);
        }
    }
}

using UnityEditor;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _obstaclesLayer;

    [Header("Instances")]
    [SerializeField] private SpriteRenderer _rend;

    private GameObject _buildingPrefab;
    private Collider2D _collider;
    private Vector2 _lastPos;
    private bool _canBuild;

    public void Setup(GameObject buildingPrefab)
    {
        _buildingPrefab = buildingPrefab;
        transform.localScale = buildingPrefab.transform.localScale;
        _rend.sprite = _buildingPrefab.GetComponent<SpriteRenderer>().sprite;
        _collider = gameObject.AddComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (_canBuild)
            _rend.color = DimOpacity(Color.green);
        else
            _rend.color = DimOpacity(Color.red);
    }

    private Color DimOpacity(Color color)
    {
        color.a = .7f;
        return color;
    }

    public void ChangePosition(Vector2 newPosition)
    {
        if (_lastPos == newPosition)
        {
            CheckForObstacles();
            return;
        }

        transform.position = newPosition;
        _lastPos = newPosition;
    }

    private void CheckForObstacles()
    {
        _canBuild = !_collider.IsTouchingLayers(_obstaclesLayer);
    }

    public void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z - 90f);
    }

    public void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z + 90f);
    }

    public void Build()
    {
        if (!_canBuild)
            return;

        GameObject o = Instantiate(_buildingPrefab, transform.position, transform.rotation);
        o.name = _buildingPrefab.name;
        Destroy(gameObject);
    }
}

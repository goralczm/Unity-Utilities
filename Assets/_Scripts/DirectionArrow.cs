using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _rotationOffset;
    [SerializeField] private Vector2 _targetOffset;
    [SerializeField] private Vector2 _padding;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Vector2 resoultion = GetResolution();
        Vector2 topRightCorner = GetTopRightCorner(resoultion);
        Vector2 bottomLeftCorner = GetBottomLeftCorner();

        Vector2 targetPositon = (Vector2)_target.position + _targetOffset;

        ClampPositionInsideScreenView(targetPositon, bottomLeftCorner, topRightCorner);

        RotateTowardsTarget(_target.position);
    }

    private Vector2 GetTopRightCorner(Vector2 resolution)
    {
        return _cam.ScreenToWorldPoint(resolution - _padding);
    }

    private Vector2 GetBottomLeftCorner()
    {
        return _cam.ScreenToWorldPoint(_padding);
    }

    private Vector2 GetResolution()
    {
        return new Vector2(Screen.width, Screen.height);
    }

    private void ClampPositionInsideScreenView(Vector2 position, Vector2 leftBottomCorner, Vector2 rightTopCorner)
    {
        float clampedX = Mathf.Clamp(position.x, leftBottomCorner.x, rightTopCorner.x);
        float clampedY = Mathf.Clamp(position.y, leftBottomCorner.y, rightTopCorner.y);
        transform.position = new Vector2(clampedX, clampedY);
    }

    private void RotateTowardsTarget(Vector2 target)
    {
        Vector2 dir = target - (Vector2)transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, rotZ + _rotationOffset);

        transform.localRotation = targetRot;
    }
}

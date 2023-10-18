using UnityEngine;

public class CameraMouseTargetFollow : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float _smoothness = 2f;
    [SerializeField, Range(0f, .5f)] private float _mouseFavoritism = .15f;

    [Header("Movement Settings")]
    [SerializeField, Range(1f, 15f)] private float _maxAcceleration = 10f;
    [SerializeField] private Vector2 _offset;

    [Header("Instances")]
    [SerializeField] private Transform _target;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        float distanceBtwTarget = Vector2.Distance(transform.position, _target.position);

        Vector3 targetPos = Vector2.Lerp(_target.position, _cam.ScreenToWorldPoint(Input.mousePosition), _mouseFavoritism);
        targetPos = (Vector2)targetPos + _offset;
        targetPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothness * Mathf.Clamp(distanceBtwTarget, 1f, _maxAcceleration) * Time.deltaTime);
    }
}

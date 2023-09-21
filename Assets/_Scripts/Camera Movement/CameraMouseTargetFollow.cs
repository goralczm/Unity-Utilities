using UnityEngine;

public class CameraMouseTargetFollow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _smoothness;
    [SerializeField, Range(0f, .5f)] private float _mouseFavoritism;

    [Header("Instances")]
    [SerializeField] private Transform _target;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Vector3 targetPos = Vector2.Lerp(_target.position, _cam.ScreenToWorldPoint(Input.mousePosition), _mouseFavoritism);
        targetPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothness * Time.deltaTime);
    }
}

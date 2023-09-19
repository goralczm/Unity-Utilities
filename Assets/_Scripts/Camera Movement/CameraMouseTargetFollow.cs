using UnityEngine;

public class CameraMouseTargetFollow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _smoothness;
    [SerializeField, Range(0f, .5f)] private float _mouseFavoritism;

    [Header("Instances")]
    [SerializeField] private Transform _target;

    private InputManager _input;

    private void Awake()
    {
        _input = InputManager.Instance;
    }

    private void Update()
    {
        Vector3 targetPos = Vector2.Lerp(_target.position, _input.MouseWorldPos, _mouseFavoritism);
        targetPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothness * Time.deltaTime);
    }
}

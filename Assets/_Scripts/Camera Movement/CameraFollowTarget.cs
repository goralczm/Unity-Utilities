using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _followSpeed;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private bool _movementOffset;
    [SerializeField] private float _movementOffsetStrength = 5f;

    [Header("Instances")]
    [SerializeField] private Transform _target;

    private InputManager _input;

    private void Awake()
    {
        _input = InputManager.Instance;
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector2 targetPos = (Vector2)_target.position + _offset;
        if (_movementOffset)
            targetPos += new Vector2(_input.HorizontalAxis * _movementOffsetStrength, 0);

        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, _followSpeed * Time.deltaTime);
        newPos.z = -10;
        transform.position = newPos;
    }
}

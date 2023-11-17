using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _followSpeed;
    [SerializeField] private Vector2 _offset;

    [Header("Heads Up Settings")]
    [SerializeField] private Vector2 _inputHeadsupStrength;
    [SerializeField, Range(0, 2f)] private float _risingVelocityHeadsupStrength;
    [SerializeField, Range(0, 2f)] private float _fallingVelocityHeadsupStrength;

    [Header("Instances")]
    [SerializeField] private UnityEngine.Transform _target;

    private Rigidbody2D _targetRb;

    private void Start()
    {
        if (_target == null)
            return;

        _targetRb = _target.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector2 inputHeadsupOffset = GetInputHeadsupOffset();
        Vector2 velocityHeadsupOffset = GetVelocityHeadsupOffset();
        Vector2 targetPos = GetTargetPos(inputHeadsupOffset, velocityHeadsupOffset);

        LerpToPosition(targetPos);
    }

    private Vector2 GetInputHeadsupOffset()
    {
        Vector2 input = GetInput();

        return input * _inputHeadsupStrength;
    }

    private Vector2 GetInput()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        return new Vector2(horizontalAxis, verticalAxis);
    }

    private Vector2 GetVelocityHeadsupOffset()
    {
        if (_targetRb == null)
            return Vector2.zero;

        Vector2 velocityOffset = new Vector2(0, _targetRb.velocity.y);

        if (_targetRb.velocity.y < 0)
            velocityOffset *= _fallingVelocityHeadsupStrength;

        if (_targetRb.velocity.y > 0)
            velocityOffset *= _risingVelocityHeadsupStrength;

        return velocityOffset;
    }

    private Vector2 GetTargetPos(Vector2 inputHeadsupOffset, Vector2 velocityHeadsupOffset)
    {
        return (Vector2)_target.position + _offset + inputHeadsupOffset + velocityHeadsupOffset;
    }

    private void LerpToPosition(Vector2 targetPos)
    {
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, _followSpeed * Time.deltaTime);
        newPos.z = -10;

        transform.position = newPos;
    }
}

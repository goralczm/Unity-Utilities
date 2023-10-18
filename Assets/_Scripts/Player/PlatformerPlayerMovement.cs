using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [Header("Jump Settings")]
    [SerializeField] private int _jumpsCount;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Instances")]
    [SerializeField] private Transform _groundCheck;

    private Rigidbody2D _rb;
    private float _horizontalAxis;
    private float _speed;
    private int _jumpsLeft;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpsLeft = _jumpsCount;
    }

    private void Update()
    {
        _horizontalAxis = PlayerInput.HorizontalAxis;
        _speed = PlayerInput.SprintTrigger ? _runSpeed : _walkSpeed;

        if (PlayerInput.JumpTrigger && CanJump())
            Jump();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalAxis * _speed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce * 500f);
        _jumpsLeft--;
    }

    private bool CanJump()
    {
        if (_jumpsLeft > 0)
            return true;

        Collider2D hit = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if (hit != null)
        {
            _jumpsLeft = _jumpsCount;
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}

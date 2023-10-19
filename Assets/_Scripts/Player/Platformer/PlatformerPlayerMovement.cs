using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _dashForce;

    [Header("Jump Settings")]
    [SerializeField] private int _bonusJumps;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _maxFallSpeed;

    [Header("Instances")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform[] _wallChecks;

    private Rigidbody2D _rb;
    private float _horizontalAxis;
    private float _verticalAxis;
    private float _speed;
    private int _jumpsLeft;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpsLeft = _bonusJumps;
    }

    private void Update()
    {
        _horizontalAxis = PlayerInput.HorizontalAxis;
        _verticalAxis = PlayerInput.VerticalAxis;
        _speed = PlayerInput.SprintTrigger ? _runSpeed : _walkSpeed;

        if (PlayerInput.DashTrigger)
            Dash();

        bool isGrounded = GroundCheck();

        if (isGrounded)
            _jumpsLeft = _bonusJumps;

        if (PlayerInput.JumpTrigger && (isGrounded || _jumpsLeft > 0))
            Jump();
    }

    private void FixedUpdate()
    {
        float currVelocityY = Mathf.Clamp(_rb.velocity.y, -_maxFallSpeed, 100f);
        _rb.velocity = new Vector2(_horizontalAxis * _speed, currVelocityY);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / 10f);
        _rb.velocity += new Vector2(0, _jumpForce);
        _jumpsLeft--;
    }

    private void Dash()
    {
        //_rb.velocity += new Vector2(_horizontalAxis, _verticalAxis) * _dashForce;
        _rb.AddForce(new Vector2(_horizontalAxis, _verticalAxis) * _dashForce);
    }

    private bool GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if (hit != null)
            return true;

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

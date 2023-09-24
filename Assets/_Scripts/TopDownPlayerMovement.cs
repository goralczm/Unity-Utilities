using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Rigidbody2D _rb;
    private float _horizontalAxis;
    private float _verticalAxis;
    private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis) * _speed;
    }
}

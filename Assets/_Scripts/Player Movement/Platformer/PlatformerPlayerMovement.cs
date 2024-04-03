using UnityEngine;
using Utilities.PlayerMovement.Input;

namespace Utilities.PlayerMovement.Platformer
{
    /// <summary>
    /// Handles the 2D platformer player movement.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollision))]
    public class PlatformerPlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _defaultGravity;
        [SerializeField] private float _movementImpairmentTime;
        [SerializeField] private float _jumpBufferTime;

        [Header("Movement Settings")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;

        [Header("Dash Settings")]
        [SerializeField] private float _dashForce;
        [SerializeField] private int _dashesCount;

        [Header("Walls Settings")]
        [SerializeField] private float _wallSlideSpeed;
        [SerializeField] private float _wallClimbingSpeed;
        [SerializeField] private float _wallJumpForce;

        [Header("Jump Settings")]
        [SerializeField] private int _jumpsCount;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _maxFallSpeed;

        private PlayerCollision _coll;
        private Rigidbody2D _rb;
        private float _speed;
        private int _jumpsLeft;
        private int _dashesLeft;
        private bool _isGrabbed;
        private bool _disableMovement;
        private float _timer;
        private float _jumpBufferTimer;
        private bool _hasJumpBuffered;

        private void Start()
        {
            _coll = GetComponent<PlayerCollision>();
            _rb = GetComponent<Rigidbody2D>();
            _jumpsLeft = _jumpsCount;
        }

        private void Update()
        {
            SetInputs();

            if (_disableMovement)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    _timer = _movementImpairmentTime;
                    _disableMovement = false;
                }
            }

            if (_coll.IsLeftWall || _coll.IsRightWall)
            {
                if (PlayerInput.JumpTrigger)
                {
                    WallJump();
                    _isGrabbed = false;
                }
            }

            if (_disableMovement)
                _isGrabbed = false;

            if (_isGrabbed)
            {
                if (PlayerInput.JumpTrigger)
                {
                    WallJump();
                    return;
                }

                SetGravity(0);
                SetVelocity(0, PlayerInput.VerticalAxisRaw * _wallClimbingSpeed);
                return;
            }

            if (!_disableMovement)
            {
                Walk();

                if (PlayerInput.DashTrigger && _dashesLeft > 0)
                {
                    Vector2 dir = new Vector2(PlayerInput.HorizontalAxisRaw, PlayerInput.VerticalAxisRaw) * _dashForce;
                    Dash(dir);
                    _dashesLeft--;
                }

                if (!_coll.IsGround)
                {
                    if ((_coll.IsLeftWall && PlayerInput.HorizontalAxisRaw == -1) ||
                        (_coll.IsRightWall && PlayerInput.HorizontalAxisRaw == 1))
                    {
                        float slideSpeed = -_wallSlideSpeed;
                        if (PlayerInput.VerticalAxisRaw == -1)
                            slideSpeed *= 3f;

                        SetVelocity(0, slideSpeed);
                    }
                }
            }

            if (_coll.IsGround)
            {
                _jumpsLeft = _jumpsCount;
                _dashesLeft = _dashesCount;

                if (_jumpBufferTime != 0 && Time.time - _jumpBufferTimer <= _jumpBufferTime)
                {
                    _hasJumpBuffered = true;
                }

                if (_hasJumpBuffered)
                {
                    Jump();
                    _hasJumpBuffered = false;
                }
            }

            if (PlayerInput.JumpTrigger)
            {
                _jumpBufferTimer = Time.time;

                if (_coll.IsGround || _jumpsLeft > 0)
                    Jump();
            }
        }

        /// <summary>
        /// Sets the internal inputs based on player inputs.
        /// </summary>
        private void SetInputs()
        {
            _speed = PlayerInput.SprintTrigger ? _runSpeed : _walkSpeed;

            if (_coll.IsLeftWall || _coll.IsRightWall)
            {
                _isGrabbed = PlayerInput.WallGrabTrigger;
            }
            else
                _isGrabbed = false;
        }

        /// <summary>
        /// Sets the velocity of rigidbody.
        /// </summary>
        /// <param name="x">The horizontal velocity.</param>
        /// <param name="y">The vertical velocity.</param>
        private void SetVelocity(float x, float y)
        {
            _rb.velocity = new Vector2(x, y);
        }

        /// <summary>
        /// Sets the gravity of rigidbody.
        /// </summary>
        /// <param name="gravity">The gravity.</param>
        private void SetGravity(float gravity)
        {
            _rb.gravityScale = gravity;
        }

        /// <summary>
        /// Clamps the rigidbody velocity to <see cref="_maxFallSpeed"/>.
        /// </summary>
        private void ClampVelocity()
        {
            float clampedY = _rb.velocity.y;
            if (_rb.velocity.y < 0)
                clampedY = Mathf.Clamp(clampedY, -_maxFallSpeed, 0);

            SetVelocity(_rb.velocity.x, clampedY);
        }

        /// <summary>
        /// Handles the player walking.
        /// </summary>
        private void Walk()
        {
            SetGravity(_defaultGravity);
            SetVelocity(PlayerInput.HorizontalAxis * _speed, _rb.velocity.y);
            ClampVelocity();
        }

        /// <summary>
        /// Handles the player jump.
        /// </summary>
        private void Jump()
        {
            SetVelocity(_rb.velocity.x, _rb.velocity.y / 10f);
            SetGravity(0);
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _jumpsLeft--;
        }

        /// <summary>
        /// Handles the player jumping being sticked to wall.
        /// </summary>
        private void WallJump()
        {
            Vector2 dir = Vector2.zero;
            if (_coll.IsLeftWall)
                dir = new Vector2(_wallJumpForce / 2f, _wallJumpForce);

            if (_coll.IsRightWall)
                dir = new Vector2(-_wallJumpForce / 2f, _wallJumpForce);

            Dash(dir);
        }

        /// <summary>
        /// Handles the player dash ability.
        /// </summary>
        /// <param name="dir">The direction of player should dash to.</param>
        private void Dash(Vector2 dir)
        {
            SetVelocity(0, 0);
            SetGravity(0);
            _rb.AddForce(dir, ForceMode2D.Impulse);
            _disableMovement = true;
        }
    }
}

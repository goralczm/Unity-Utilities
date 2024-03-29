using UnityEngine;

namespace Utilities.PlayerMovement.Platformer
{
    /// <summary>
    /// Handles the player collision logic.
    /// </summary>
    public class PlayerCollision : MonoBehaviour
    {
        public bool IsGround => GroundCheck();
        public bool IsLeftWall => LeftWallCheck();
        public bool IsRightWall => RightWallCheck();

        [Header("Settings")]
        [SerializeField] private LayerMask _groundLayer;

        [Header("Radius")]
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _wallCheckRadius;

        [Header("Checks")]
        [SerializeField] private UnityEngine.Transform[] _groundChecks;
        [SerializeField] private UnityEngine.Transform[] _leftWallChecks;
        [SerializeField] private UnityEngine.Transform[] _rightWallChecks;

        /// <summary>
        /// Checks if the player is currently on ground.
        /// </summary>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool GroundCheck()
        {
            foreach (UnityEngine.Transform groundCheck in _groundChecks)
            {
                Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, _groundCheckRadius, _groundLayer);
                if (hit != null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the player is currently colliding with a wall on his left side.
        /// </summary>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool LeftWallCheck()
        {
            foreach (UnityEngine.Transform leftWallCheck in _leftWallChecks)
            {
                Collider2D hit = Physics2D.OverlapCircle(leftWallCheck.position, _wallCheckRadius, _groundLayer);
                if (hit != null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the player is currently colliding with a wall on his right side.
        /// </summary>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool RightWallCheck()
        {
            foreach (UnityEngine.Transform rightWallCheck in _rightWallChecks)
            {
                Collider2D hit = Physics2D.OverlapCircle(rightWallCheck.position, _wallCheckRadius, _groundLayer);
                if (hit != null)
                    return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            if (_leftWallChecks != null && _leftWallChecks.Length > 0)
            {
                Gizmos.color = Color.yellow;
                foreach (UnityEngine.Transform leftWallCheck in _leftWallChecks)
                    Gizmos.DrawWireSphere(leftWallCheck.position, _wallCheckRadius);
            }

            if (_rightWallChecks != null && _rightWallChecks.Length > 0)
            {
                Gizmos.color = Color.yellow;
                foreach (UnityEngine.Transform rightWallCheck in _rightWallChecks)
                    Gizmos.DrawWireSphere(rightWallCheck.position, _wallCheckRadius);
            }

            if (_groundChecks != null && _groundChecks.Length > 0)
            {
                Gizmos.color = Color.red;
                foreach (UnityEngine.Transform groundCheck in _groundChecks)
                    Gizmos.DrawWireSphere(groundCheck.position, _groundCheckRadius);
            }
        }
    }
}

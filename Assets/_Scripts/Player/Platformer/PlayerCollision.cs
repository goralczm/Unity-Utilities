using UnityEngine;

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
    [SerializeField] private Transform[] _groundChecks;
    [SerializeField] private Transform[] _leftWallChecks;
    [SerializeField] private Transform[] _rightWallChecks;

    private bool GroundCheck()
    {
        foreach (Transform groundCheck in _groundChecks)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, _groundCheckRadius, _groundLayer);
            if (hit != null)
                return true;
        }

        return false;
    }

    private bool LeftWallCheck()
    {
        foreach (Transform leftWallCheck in _leftWallChecks)
        {
            Collider2D hit = Physics2D.OverlapCircle(leftWallCheck.position, _wallCheckRadius, _groundLayer);
            if (hit != null)
                return true;
        }

        return false;
    }

    private bool RightWallCheck()
    {
        foreach (Transform rightWallCheck in _rightWallChecks)
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
            foreach (Transform leftWallCheck in _leftWallChecks)
                Gizmos.DrawWireSphere(leftWallCheck.position, _wallCheckRadius);
        }

        if (_rightWallChecks != null && _rightWallChecks.Length > 0)
        {
            Gizmos.color = Color.yellow;
            foreach (Transform rightWallCheck in _rightWallChecks)
                Gizmos.DrawWireSphere(rightWallCheck.position, _wallCheckRadius);
        }

        if (_groundChecks != null && _groundChecks.Length > 0)
        {
            Gizmos.color = Color.red;
            foreach (Transform groundCheck in _groundChecks)
                Gizmos.DrawWireSphere(groundCheck.position, _groundCheckRadius);
        }
    }
}

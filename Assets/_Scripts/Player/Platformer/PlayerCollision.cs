using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform[] _leftWallChecks;
    [SerializeField] private Transform[] _rightWallChecks;

    private bool GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if (hit != null)
            return true;

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
        if (_groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);

        Gizmos.color = Color.yellow;
        foreach (Transform leftWallCheck in _leftWallChecks)
            Gizmos.DrawWireSphere(leftWallCheck.position, _wallCheckRadius);

        foreach (Transform rightWallCheck in _rightWallChecks)
            Gizmos.DrawWireSphere(rightWallCheck.position, _wallCheckRadius);
    }
}

using UnityEngine;

public class RandomInsideRectangle : MonoBehaviour
{
    [SerializeField] private float _width, _height;

    public Vector2 GetRandomPositionInside()
    {
        Vector2 bottomLeftCorner = GetBottomLeftCorner();
        Vector2 upperRightCorner = GetUpperRightCorner();

        return new Vector2(Random.Range(bottomLeftCorner.x, upperRightCorner.x),
                           Random.Range(bottomLeftCorner.y, upperRightCorner.y));
    }

    private Vector2 GetBottomLeftCorner()
    {
        return new Vector2(transform.position.x - _width / 2f, transform.position.y - _height / 2f);
    }

    private Vector2 GetUpperRightCorner()
    {
        return new Vector2(transform.position.x + _width / 2f, transform.position.y + _height / 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector2 bottomLeftCorner = GetBottomLeftCorner();
        Gizmos.DrawLine(bottomLeftCorner, bottomLeftCorner + new Vector2(_width, 0));
        Gizmos.DrawLine(bottomLeftCorner, bottomLeftCorner + new Vector2(0, _height));
        
        Vector2 upperRightCorner = GetUpperRightCorner();
        Gizmos.DrawLine(upperRightCorner, upperRightCorner - new Vector2(_width, 0));
        Gizmos.DrawLine(upperRightCorner, upperRightCorner - new Vector2(0, _height));
    }
}

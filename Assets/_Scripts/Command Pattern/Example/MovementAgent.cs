using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    public Vector2 Destination { get; private set; }

    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Destination, Time.deltaTime * _speed);
    }

    public void SetDestination(Vector2 position)
    {
        Destination = position;
    }
}

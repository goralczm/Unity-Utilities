using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private SpriteRenderer _rend;

    private void Start()
    {
        UpdateRenderer();
    }

    public int GetHealth() => health;

    private void UpdateRenderer()
    {
        _rend.color = new Color(1f, health / 10f, health / 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);

        if (collision.name == "Mouse")
            health--;

        if (collision.name == "Healthpod")
            health = 10;

        UpdateRenderer();
    }
}

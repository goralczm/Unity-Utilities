using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _interactionRadius;

    public float InteractionRadius => _interactionRadius;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactionRadius);
            foreach (Collider2D hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable == null)
                    continue;

                interactable.Interact(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}

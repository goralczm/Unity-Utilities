using UnityEngine;

public class ProximityInteractionHandler : InteractionHandler
{
    protected override void HandleInteractionInput()
    {
        if (!Input.GetButtonDown("Interaction"))
            return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactionRadius);
        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable == null)
                continue;

            Interact(interactable, hit.transform);
            break;
        }
    }
}

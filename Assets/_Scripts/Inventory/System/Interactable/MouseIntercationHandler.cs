using UnityEngine;

public class MouseIntercationHandler : InteractionHandler
{
    protected override void HandleInteractionInput()
    {
        if (!Input.GetButtonDown("Interaction"))
            return;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider == null)
            return;

        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if (interactable == null)
            return;

        Interact(interactable, hit.transform);
    }
}

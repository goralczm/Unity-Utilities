using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionHandler : MonoBehaviour
{
    [SerializeField] protected float _interactionRadius = 1.5f;

    private KeyValuePair<IInteractable, Transform> _lastInteractable;
    private float _interactionDistance;

    private void Update()
    {
        HandleInteractionInput();

        HandleOutOfRangeInteractable();
    }

    protected abstract void HandleInteractionInput();

    protected void Interact(IInteractable interactable, Transform origin)
    {
        _lastInteractable = new KeyValuePair<IInteractable, Transform>(interactable, origin);
        _interactionDistance = Vector2.Distance(transform.position, origin.position);
        interactable.Interact(gameObject);
    }

    public void ResetInteractable()
    {
        _lastInteractable = new KeyValuePair<IInteractable, Transform>();
    }

    private void HandleOutOfRangeInteractable()
    {
        if (_lastInteractable.Key == null)
            return;

        float distanceFromInteractable = Vector2.Distance(_lastInteractable.Value.position, transform.position);
        if (distanceFromInteractable > Mathf.Max(_interactionDistance, _interactionRadius))
        {
            _lastInteractable.Key.OutOfRange();
            _lastInteractable = new KeyValuePair<IInteractable, Transform>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}

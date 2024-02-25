using UnityEngine;

public interface IInteractable
{
    public void Interact(GameObject requester);
    public void OutOfRange();
}
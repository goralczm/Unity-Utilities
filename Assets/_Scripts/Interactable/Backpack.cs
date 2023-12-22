using UnityEngine;

public class Backpack : Inventory, IInteractable
{
    public void Interact(GameObject requester)
    {
        BackpackManager.Instance.ShowBackpack(this);
    }
}

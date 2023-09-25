using UnityEngine;

public class Backpack : Inventory, IInteractable
{
    public void Interact()
    {
        BackpackManager.Instance.ShowBackpack(this);
    }
}

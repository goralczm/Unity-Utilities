using UnityEngine;

public class Backpack : Inventory, IInteractable
{
    public void Interact()
    {
        BackpackUi.Instance.ShowBackpack(this);
    }
}

using UnityEngine;

public class Backpack : Inventory, IInteractable
{
    private BackpackManager _backpack;

    private void Start()
    {
        _backpack = BackpackManager.Instance;
    }

    public void Interact(GameObject requester)
    {
        _backpack.ShowBackpack(this);
    }

    public void OutOfRange()
    {
        _backpack.HideBackpack();
    }
}

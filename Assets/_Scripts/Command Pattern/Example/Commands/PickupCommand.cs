using UnityEngine;

public class PickupCommand : Command
{
    public override bool IsFinished => true;

    private readonly Inventory _inventory;
    private readonly ItemPickup _itemPickup;

    public PickupCommand(Inventory inventory, ItemPickup itemPickup)
    {
        _inventory = inventory;
        _itemPickup = itemPickup;
    }

    public override void Execute()
    {
        _itemPickup.Interact(_inventory.gameObject);
    }

    public override void Tick()
    {

    }

    public override void Undo()
    {
        _inventory.RemoveItem(_itemPickup._item, 1);
        _itemPickup.gameObject.SetActive(true);
    }
}
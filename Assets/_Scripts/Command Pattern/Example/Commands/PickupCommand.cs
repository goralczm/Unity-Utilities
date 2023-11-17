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
        _inventory.AddItem(_itemPickup.Item, 1);
        Object.Destroy(_itemPickup.gameObject);
    }

    public override void Undo()
    {
        _inventory.RemoveItem(_itemPickup.Item, 1);
    }
}

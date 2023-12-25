using UnityEngine;

public class PickupCommand : Command
{
    public override bool IsFinished => true;

    private readonly Inventory _inventory;
    private readonly ItemPickup _itemPickup;

    private Item _item;

    public PickupCommand(Inventory inventory, ItemPickup itemPickup)
    {
        _inventory = inventory;
        _itemPickup = itemPickup;
    }

    public override void Execute()
    {
        _item = _itemPickup.GetItem();
        _itemPickup.Interact(_inventory.gameObject);
    }

    public override void Tick()
    {

    }

    public override void Undo()
    {
        _inventory.RemoveItem(_item, 1);
        _item.OnDrop(_inventory.transform.position);
    }
}

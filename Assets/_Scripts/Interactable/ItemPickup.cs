using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] public Item _item;

    private Inventory _inventoryToAdd;

    public void Interact()
    {
        AddToInventory();
        gameObject.SetActive(false);
    }

    private void AddToInventory()
    {
        if (_inventoryToAdd == null)
            return;

        _inventoryToAdd.AddItem(_item, 1);
    }

    public void SetInventory(Inventory inventory)
    {
        _inventoryToAdd = inventory;
    }
}

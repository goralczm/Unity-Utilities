using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _capacity = 9;

    public List<InventoryItem> items = new List<InventoryItem>();

    public delegate void ItemsChangedDelegate();
    public ItemsChangedDelegate ItemsChangedHandler;

    private void Awake()
    {
        for (int i = 0; i < _capacity; i++)
            items.Add(new InventoryItem());
    }

    public void AddItem(Item newItem, int amount)
    {
        if (newItem == null)
            return;

        if (amount == 0)
            return;

        try
        {
            int foundIndex = ReturnItemIndexWithFreeSpace(newItem);
            AddItemToExistingSlot(newItem, amount, foundIndex);
        }
        catch
        {
            AddItemToEmptySlot(newItem, amount);
        }

        InvokeOnItemChangedHandler();
    }

    private int ReturnItemIndexWithFreeSpace(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item != item)
                continue;

            if (items[i].quantity == items[i].item.stackSize)
                continue;

            return i;
        }

        throw new IndexNotFoundException("No item with free space!");
    }

    public void AddItemToExistingSlot(Item newItem, int amount, int foundIndex)
    {
        int totalItemsAmount = items[foundIndex].quantity + amount;
        items[foundIndex] = new InventoryItem(newItem, Mathf.Min(newItem.stackSize, totalItemsAmount));

        InvokeOnItemChangedHandler();

        if (totalItemsAmount > newItem.stackSize)
            AddItem(newItem, totalItemsAmount - newItem.stackSize);
    }

    private void AddItemToEmptySlot(Item newItem, int amount)
    {
        try
        {
            int firstEmptySlotIndex = ReturnFirstEmptySlotIndex();
            int overflowAmount = amount - newItem.stackSize;
            items[firstEmptySlotIndex] = new InventoryItem(newItem, Mathf.Min(newItem.stackSize, amount));

            if (overflowAmount > 0)
                AddItem(newItem, overflowAmount);
        }
        catch
        {

        }
    }

    private int ReturnFirstEmptySlotIndex()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == null)
                return i;
        }

        throw new IndexNotFoundException("No empty slots!");
    }

    public void RemoveItem(Item itemToRemove, int amount)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].item == itemToRemove)
            {
                int amountAfterRemove = items[i].quantity - amount;

                if (amountAfterRemove <= 0)
                {
                    items[i] = new InventoryItem();

                    if (amountAfterRemove < 0)
                        RemoveItem(itemToRemove, Mathf.Abs(amountAfterRemove));
                }
                else
                    items[i] = new InventoryItem(itemToRemove, amountAfterRemove);
                break;
            }
        }

        InvokeOnItemChangedHandler();
    }

    public void RemoveItemFromIndex(int index)
    {
        items[index] = new InventoryItem();

        InvokeOnItemChangedHandler();
    }

    public InventoryItem SwapInventoryItems(InventoryItem newItem, int indexToSwap)
    {
        InventoryItem itemToSwap = items[indexToSwap];
        items[indexToSwap] = newItem;

        InvokeOnItemChangedHandler();

        return itemToSwap;
    }

    public void DivideItem(int indexToDivide)
    {
        if (items[indexToDivide].quantity == 1)
            return;

        try
        {
            int firstEmptySlotIndex = ReturnFirstEmptySlotIndex();
            int amountAfterDivision = Mathf.FloorToInt(items[indexToDivide].quantity / 2);

            items[indexToDivide] = new InventoryItem(items[indexToDivide].item, items[indexToDivide].quantity - amountAfterDivision);
            items[firstEmptySlotIndex] = new InventoryItem(items[indexToDivide].item, amountAfterDivision);
        }
        catch
        {

        }

        InvokeOnItemChangedHandler();
    }

    public int CountItemOccurrences(Item item)
    {
        int sum = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item != item)
                continue;

            sum += items[i].quantity;
        }

        return sum;
    }

    public void InvokeOnItemChangedHandler()
    {
        ItemsChangedHandler?.Invoke();
    }

    public bool CanAdd(Item newItem, int quantity)
    {
        foreach (InventoryItem item in items)
        {
            if (item.item == null)
                quantity -= newItem.stackSize;

            if (item.item == newItem)
                quantity -= newItem.stackSize - item.quantity;

            if (quantity <= 0)
                return true;
        }

        return false;
    }

    public void DropItemFromSlot(int slotIndex)
    {
        if (items[slotIndex].item == null)
            return;

        items[slotIndex].item.OnDrop(transform.position);
        RemoveItemFromIndex(slotIndex);
    }
}

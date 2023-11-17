using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _capacity;

    public KeyValuePair<Item, int>[] items;

    public delegate void ItemsChangedDelegate();
    public ItemsChangedDelegate itemsChangedHandler;

    private void Awake()
    {
        items = new KeyValuePair<Item, int>[_capacity];
    }

    public void AddItem(Item newItem, int amount)
    {
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
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Key != item)
                continue;

            if (items[i].Value == items[i].Key.stackSize)
                continue;

            return i;
        }

        throw new IndexNotFoundException("No item with free space!");
    }

    public void AddItemToExistingSlot(Item newItem, int amount, int foundIndex)
    {
        int totalItemsAmount = items[foundIndex].Value + amount;
        items[foundIndex] = new KeyValuePair<Item, int>(newItem, Mathf.Min(items[foundIndex].Key.stackSize, totalItemsAmount));

        InvokeOnItemChangedHandler();

        if (totalItemsAmount > items[foundIndex].Key.stackSize)
            AddItem(newItem, totalItemsAmount - items[foundIndex].Key.stackSize);
    }

    private void AddItemToEmptySlot(Item newItem, int amount)
    {
        try
        {
            int firstEmptySlotIndex = ReturnFirstEmptySlotIndex();
            int overflowAmount = amount - newItem.stackSize;
            items[firstEmptySlotIndex] = new KeyValuePair<Item, int>(newItem, Mathf.Min(newItem.stackSize, amount));

            if (overflowAmount > 0)
                AddItem(newItem, overflowAmount);
        }
        catch
        {

        }
    }

    private int ReturnFirstEmptySlotIndex()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Key == null)
                return i;
        }

        throw new IndexNotFoundException("No empty slots!");
    }

    public void RemoveItem(Item itemToRemove, int amount)
    {
        for (int i = items.Length - 1; i >= 0; i--)
        {
            if (items[i].Key == itemToRemove)
            {
                int amountAfterRemove = items[i].Value - amount;

                if (amountAfterRemove <= 0)
                {
                    items[i] = new KeyValuePair<Item, int>();

                    if (amountAfterRemove < 0)
                        RemoveItem(itemToRemove, Mathf.Abs(amountAfterRemove));
                }
                else
                    items[i] = new KeyValuePair<Item, int>(itemToRemove, amountAfterRemove);
                break;
            }
        }

        InvokeOnItemChangedHandler();
    }

    public void RemoveItemFromIndex(int index)
    {
        items[index] = new KeyValuePair<Item, int>();

        InvokeOnItemChangedHandler();
    }

    public KeyValuePair<Item, int> SwapInventoryItems(KeyValuePair<Item, int> newItem, int indexToSwap)
    {
        KeyValuePair<Item, int> itemToSwap = items[indexToSwap];
        items[indexToSwap] = newItem;

        InvokeOnItemChangedHandler();

        return itemToSwap;
    }

    public void DivideItem(int indexToDivide)
    {
        if (items[indexToDivide].Value == 1)
            return;

        try
        {
            int firstEmptySlotIndex = ReturnFirstEmptySlotIndex();
            int amountAfterDivision = Mathf.FloorToInt(items[indexToDivide].Value / 2);

            items[indexToDivide] = new KeyValuePair<Item, int>(items[indexToDivide].Key, items[indexToDivide].Value - amountAfterDivision);
            items[firstEmptySlotIndex] = new KeyValuePair<Item, int>(items[indexToDivide].Key, amountAfterDivision);
        }
        catch
        {

        }

        InvokeOnItemChangedHandler();
    }

    public int CountItemOccurrences(Item item)
    {
        int sum = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Key != item)
                continue;

            sum += items[i].Value;
        }

        return sum;
    }

    private void InvokeOnItemChangedHandler()
    {
        if (itemsChangedHandler != null)
            itemsChangedHandler.Invoke();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _capacity;

    public Dictionary<Item, int> items;

    public delegate void ItemsChangedDelegate();
    public ItemsChangedDelegate itemsChangedHandler;

    private void Start()
    {
        items = new Dictionary<Item, int>();
    }

    public void AddItem(Item newItem)
    {
        if (items.Count == _capacity)
            return;

        if (items.ContainsKey(newItem))
        {
            if (items[newItem] == newItem.stackSize)
                return;

            items[newItem] += 1;
        }
        else
            items.Add(newItem, 1);

        if (itemsChangedHandler != null)
            itemsChangedHandler.Invoke();
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (!items.ContainsKey(itemToRemove))
            return;

        if (items[itemToRemove] == 1)
            items.Remove(itemToRemove);
        else
            items[itemToRemove] -= 1;

        if (itemsChangedHandler != null)
            itemsChangedHandler.Invoke();
    }
}

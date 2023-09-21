using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _capacity;

    public List<KeyValuePair<Item, int>> items;

    public delegate void ItemsChangedDelegate();
    public ItemsChangedDelegate itemsChangedHandler;

    private void Start()
    {
        items = new List<KeyValuePair<Item, int>>();
    }

    public void AddItem(Item newItem)
    {
        bool addedItem = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == newItem)
            {
                if (items[i].Value == newItem.stackSize)
                    continue;

                items[i] = new KeyValuePair<Item, int>(newItem, items[i].Value + 1);
                addedItem = true;
                break;
            }
        }

        if (items.Count == _capacity)
            return;

        if (!addedItem)
        {
            KeyValuePair<Item, int> newPair = new KeyValuePair<Item, int>(newItem, 1);
            items.Add(newPair);
        }

        if (itemsChangedHandler != null)
            itemsChangedHandler.Invoke();
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == itemToRemove)
            {
                if (items[i].Value == 1)
                {
                    items.RemoveAt(i);
                    break;
                }

                items[i] = new KeyValuePair<Item, int>(itemToRemove, items[i].Value - 1);
                break;
            }
        }

        if (itemsChangedHandler != null)
            itemsChangedHandler.Invoke();
    }
}

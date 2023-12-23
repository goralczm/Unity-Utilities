using System.Collections.Generic;
using UnityEngine;

public class StartingItems : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private List<InventoryItem> _items;

    private void Start()
    {
        if (_inventory == null)
            return;

        if (_items.Count == 0)
            return;

        foreach (InventoryItem item in _items)
            _inventory.AddItem(item.item, item.quantity);
    }
}

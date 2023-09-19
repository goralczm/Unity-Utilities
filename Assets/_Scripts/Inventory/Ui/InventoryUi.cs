using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Inventory _inventory;

    [Header("Instances")]
    [SerializeField] private InventorySlot[] _inventorySlots;


    private void Start()
    {
        ResetSlots();

        _inventory.itemsChangedHandler += UpdateUi;
    }

    private void UpdateUi()
    {
        ResetSlots();

        int i = 0;
        foreach (KeyValuePair<Item, int> itemAndAmount in _inventory.items)
        {
            if (i > _inventorySlots.Length - 1)
                return;

            _inventorySlots[i].SetupSlotUi(itemAndAmount.Key, itemAndAmount.Value);

            i++;
        }
    }

    private void ResetSlots()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].ResetSlot();
        }
    }
}

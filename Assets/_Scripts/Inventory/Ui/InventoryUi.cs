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
        for (int i = 0; i < _inventory.items.Length; i++)
        {
            if (_inventory.items[i].Key == null)
            {
                _inventorySlots[i].ResetSlot();
                continue;
            }

            _inventorySlots[i].SetupInfo(_inventory, i);
            _inventorySlots[i].SetupSlotUi(_inventory.items[i].Key, _inventory.items[i].Value);
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

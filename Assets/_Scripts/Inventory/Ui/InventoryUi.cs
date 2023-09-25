using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [Header("Settings")]
    public Inventory _inventory;
    public bool _autoSetup;

    [Header("Instances")]
    [SerializeField] private List<InventorySlot> _inventorySlots;


    private void Start()
    {
        ResetSlots();

        if (_inventory == null)
            return;

        SetNewInventory(_inventory);
    }

    private void UpdateUi()
    {
        ResetSlots();

        for (int i = 0; i < _inventory.items.Length; i++)
        {
            if (_autoSetup && i > _inventorySlots.Count - 1)
                _inventorySlots.Add(Instantiate(_inventorySlots[0], _inventorySlots[0].transform.parent));

            _inventorySlots[i].SetupSlotUi(_inventory.items[i].Key, _inventory.items[i].Value);
            _inventorySlots[i].gameObject.SetActive(true);
        }
    }

    private void ResetSlots()
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].ResetSlot();
            _inventorySlots[i].SetupInfo(_inventory, i);
            _inventorySlots[i].gameObject.SetActive(false);
        }
    }

    public void SetNewInventory(Inventory newInventory)
    {
        if (_inventory != null)
            _inventory.itemsChangedHandler -= UpdateUi;

        _inventory = newInventory;
        _inventory.itemsChangedHandler += UpdateUi;

        UpdateUi();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [Header("Settings")]
    public Inventory inventory;
    public bool autoSetup;

    [Header("Instances")]
    [SerializeField] private List<InventorySlot> _inventorySlots;


    private void Start()
    {
        ResetSlots();

        if (inventory == null)
            return;

        SetNewInventory(inventory);
    }

    private void UpdateUi()
    {
        ResetSlots();

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (autoSetup && i > _inventorySlots.Count - 1)
                _inventorySlots.Add(Instantiate(_inventorySlots[0], _inventorySlots[0].transform.parent));

            _inventorySlots[i].SetupSlotUi(inventory.items[i].Key, inventory.items[i].Value);
            _inventorySlots[i].gameObject.SetActive(true);
        }
    }

    private void ResetSlots()
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].ResetSlot();
            _inventorySlots[i].SetupInfo(inventory, i);
            _inventorySlots[i].gameObject.SetActive(false);
        }
    }

    public void SetNewInventory(Inventory newInventory)
    {
        if (inventory != null)
            inventory.itemsChangedHandler -= UpdateUi;

        inventory = newInventory;
        inventory.itemsChangedHandler += UpdateUi;

        UpdateUi();
    }
}

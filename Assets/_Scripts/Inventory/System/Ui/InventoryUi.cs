using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [Header("Settings")]
    public Inventory inventory;
    [SerializeField] private bool autoSetup;
    [SerializeField] private bool _canDropItems;

    [Header("Instances")]
    [SerializeField] private Transform[] _inventorySlotsParents;

    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    private void Start()
    {
        SetSlots();
        ResetSlots();

        if (inventory == null)
            return;

        SetNewInventory(inventory);
    }

    private void SetSlots()
    {
        for (int i = 0; i < _inventorySlotsParents.Length; i++)
        {
            for (int j = 0; j < _inventorySlotsParents[i].childCount; j++)
            {
                _inventorySlots.Add(_inventorySlotsParents[i].GetChild(j).GetComponent<InventorySlot>());
            }
        }
    }

    private void UpdateUi()
    {
        ResetSlots();

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (autoSetup && i > _inventorySlots.Count - 1)
                _inventorySlots.Add(Instantiate(_inventorySlots[0], _inventorySlots[0].transform.parent));

            _inventorySlots[i].ResetSlot();
            _inventorySlots[i].SetupSlotUi(inventory.items[i].item, inventory.items[i].quantity);
            _inventorySlots[i].gameObject.SetActive(true);
        }
    }

    private void ResetSlots()
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].ResetSlot();
            _inventorySlots[i].SetupInfo(inventory, i);
            _inventorySlots[i].canDropItems = _canDropItems;
            _inventorySlots[i].gameObject.SetActive(false);
        }
    }

    public void SetNewInventory(Inventory newInventory)
    {
        if (inventory != null)
            inventory.ItemsChangedHandler -= UpdateUi;

        inventory = newInventory;
        inventory.ItemsChangedHandler += UpdateUi;

        UpdateUi();
    }
}

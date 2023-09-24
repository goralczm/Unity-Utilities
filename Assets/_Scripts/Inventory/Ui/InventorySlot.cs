using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;

    private Inventory _inventory;
    private int _slotIndex;
    private KeyValuePair<Item, int> _item;

    public void SetupInfo(Inventory newInventory, int slotIndex)
    {
        _inventory = newInventory;
        _slotIndex = slotIndex;
    }

    public void SetupSlotUi(Item newItem, int amount)
    {
        _item = new KeyValuePair<Item, int>(newItem, amount);

        _amountText.SetText(amount.ToString());
        _icon.sprite = newItem.icon;
        _icon.enabled = true;
    }

    public void ResetSlot()
    {
        _item = new KeyValuePair<Item, int>();
        _inventory = null;
        _amountText.SetText("");
        _icon.sprite = null;
        _icon.enabled = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
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

        if (newItem == null)
            return;

        _amountText.SetText(amount.ToString());
        _icon.sprite = newItem.icon;
        _icon.enabled = true;
    }

    public void ResetSlot()
    {
        _item = new KeyValuePair<Item, int>();
        _amountText.SetText("");
        _icon.enabled = false;
    }

    public void UseItem()
    {
        if (_item.Key == null)
            return;

        _item.Key.UseItem();
    }

    public KeyValuePair<Item, int> SwapItems(KeyValuePair<Item, int> newItem)
    {
        return _inventory.SwapInventoryItems(newItem, _slotIndex);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        InventorySlot draggedSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();
        if (draggedSlot._item.Key == _item.Key)
        {
            if (draggedSlot._item.Value != draggedSlot._item.Key.stackSize && _item.Value != _item.Key.stackSize)
            {
                _inventory.AddItemToExistingSlot(draggedSlot._item.Key, draggedSlot._item.Value, _slotIndex);
                draggedSlot._inventory.RemoveItemFromIndex(draggedSlot._slotIndex);
                return;
            }
        }

        KeyValuePair<Item, int> swappedItem = draggedSlot.SwapItems(_item);
        SwapItems(swappedItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        _inventory.DivideItem(_slotIndex);
    }
}

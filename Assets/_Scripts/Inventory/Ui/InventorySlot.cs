using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("Instances")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;

    private Inventory _inventory;
    private int _slotIndex;
    private KeyValuePair<Item, int> _item;

    private BackpackManager _backpackManager;
    private GameManager _gameManager;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                return;

            if (_backpackManager == null)
                _backpackManager = BackpackManager.Instance;

            if (_backpackManager.CurrentInventory == null)
                return;

            FastMoveItems();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
            _inventory.DivideItem(_slotIndex);
    }

    private void FastMoveItems()
    {
        Inventory inventoryToMove = _backpackManager.CurrentInventory;
        if (_backpackManager.CurrentInventory == _inventory)
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            inventoryToMove = _gameManager.PlayerInventory;
        }

        Item itemBeingMoved = _item.Key;
        int amountBeingMoved = _item.Value;

        int totalItemsBeforeMove = inventoryToMove.CountItemOccurrences(itemBeingMoved);
        inventoryToMove.AddItem(itemBeingMoved, amountBeingMoved);
        int totalItemsAfterMove = inventoryToMove.CountItemOccurrences(itemBeingMoved);

        _inventory.RemoveItemFromIndex(_slotIndex);
        if (totalItemsAfterMove - totalItemsBeforeMove < amountBeingMoved)
            _inventory.AddItem(itemBeingMoved, amountBeingMoved - (totalItemsAfterMove - totalItemsBeforeMove));
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        InventorySlot draggedSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();

        if (draggedSlot == this)
            return;

        Item draggedItem = draggedSlot._item.Key;

        if (!AreItemsSame(draggedItem, _item.Key) || IsSlotFull(draggedSlot) || IsSlotFull(this))
        {
            SwapInventoryItems(draggedSlot);
            return;
        }

        AddDraggedItemToInventory(draggedSlot);
    }

    private bool AreItemsSame(Item firstItem, Item secondItem)
    {
        return firstItem == secondItem;
    }

    private bool IsSlotFull(InventorySlot inventorySlot)
    {
        if (inventorySlot._item.Key == null)
            return false;

        return inventorySlot._item.Value == inventorySlot._item.Key.stackSize;
    }

    private void SwapInventoryItems(InventorySlot draggedSlot)
    {
        KeyValuePair<Item, int> swappedItem = draggedSlot.SwapItems(_item);
        SwapItems(swappedItem);
    }

    public KeyValuePair<Item, int> SwapItems(KeyValuePair<Item, int> newItem)
    {
        return _inventory.SwapInventoryItems(newItem, _slotIndex);
    }

    private void AddDraggedItemToInventory(InventorySlot draggedSlot)
    {
        Item draggedItem = draggedSlot._item.Key;
        int draggedAmount = draggedSlot._item.Value;

        int totalItemsBeforeAddition = _inventory.CountItemOccurrences(draggedItem);
        _inventory.AddItemToExistingSlot(draggedItem, draggedAmount, _slotIndex);
        int totalItemsAfterAddition = _inventory.CountItemOccurrences(draggedItem);

        draggedSlot._inventory.RemoveItemFromIndex(draggedSlot._slotIndex);
        if (totalItemsAfterAddition - totalItemsBeforeAddition < draggedAmount)
            draggedSlot._inventory.AddItem(draggedItem, draggedAmount - (totalItemsAfterAddition - totalItemsBeforeAddition));
    }
}

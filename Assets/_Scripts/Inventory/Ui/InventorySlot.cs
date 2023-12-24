using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem Item { get; private set; }

    [Header("Instances")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _deleteButton;

    private Inventory _inventory;
    private int _slotIndex;

    private BackpackManager _backpackManager;
    private GameManager _gameManager;

    public Action OnSlotChangedHandler;

    public void SetupInfo(Inventory newInventory, int slotIndex)
    {
        _inventory = newInventory;
        _slotIndex = slotIndex;
    }

    public void SetupSlotUi(Item newItem, int amount)
    {
        Item = new InventoryItem(newItem, amount);

        if (newItem == null)
            return;

        if (amount == 1)
            _amountText.SetText("");
        else
            _amountText.SetText(amount.ToString());
        _icon.sprite = newItem.icon;
        _icon.enabled = true;

        OnSlotChangedHandler?.Invoke();
    }

    public void ResetSlot()
    {
        Item = new InventoryItem();
        _amountText.SetText("");
        _icon.enabled = false;
        if (_deleteButton != null)
            _deleteButton.SetActive(false);
    }

    public void UseItem()
    {
        if (Item.item == null)
            return;

        Item.item.UseItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item.item == null)
            return;

        if (_deleteButton != null)
            _deleteButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_deleteButton != null)
            _deleteButton.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                return;

            if (_backpackManager == null)
                _backpackManager = BackpackManager.Instance;

            if (_backpackManager == null || _backpackManager.CurrentInventory == null)
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

        Item itemBeingMoved = Item.item;
        int amountBeingMoved = Item.quantity;

        int totalItemsBeforeMove = inventoryToMove.CountItemOccurrences(itemBeingMoved);
        inventoryToMove.AddItem(itemBeingMoved, amountBeingMoved);
        int totalItemsAfterMove = inventoryToMove.CountItemOccurrences(itemBeingMoved);

        int itemsAccuallyMoved = totalItemsAfterMove - totalItemsBeforeMove;

        _inventory.RemoveItemFromIndex(_slotIndex);
        if (itemsAccuallyMoved < amountBeingMoved)
            _inventory.AddItem(itemBeingMoved, amountBeingMoved - itemsAccuallyMoved);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        InventorySlot draggedSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();

        if (draggedSlot == this)
            return;

        Item draggedItem = draggedSlot.Item.item;

        if (!AreItemsSame(draggedItem, Item.item) || IsSlotFull(draggedSlot) || IsSlotFull(this))
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
        if (inventorySlot.Item.item == null)
            return false;

        return inventorySlot.Item.quantity == inventorySlot.Item.item.stackSize;
    }

    private void SwapInventoryItems(InventorySlot draggedSlot)
    {
        InventoryItem swappedItem = draggedSlot.SwapItems(Item);
        SwapItems(swappedItem);
    }

    public InventoryItem SwapItems(InventoryItem newItem)
    {
        return _inventory.SwapInventoryItems(newItem, _slotIndex);
    }

    private void AddDraggedItemToInventory(InventorySlot draggedSlot)
    {
        Item draggedItem = draggedSlot.Item.item;
        int draggedAmount = draggedSlot.Item.quantity;

        draggedSlot._inventory.RemoveItemFromIndex(draggedSlot._slotIndex);
        int overflow = _inventory.items[_slotIndex].quantity + draggedAmount - draggedItem.stackSize;
        if (overflow > 0)
            draggedSlot._inventory.AddItemToExistingSlot(draggedItem, overflow, draggedSlot._slotIndex);

        _inventory.AddItemToExistingSlot(draggedItem, Mathf.Min(draggedAmount, draggedAmount - overflow), _slotIndex);
        /*int totalItemsBeforeAddition = _inventory.CountItemOccurrences(draggedItem);
        _inventory.AddItemToExistingSlot(draggedItem, draggedAmount, _slotIndex);
        int totalItemsAfterAddition = _inventory.CountItemOccurrences(draggedItem);

        int itemsAccuallyMoved = totalItemsAfterAddition - totalItemsBeforeAddition;

        draggedSlot._inventory.RemoveItemFromIndex(draggedSlot._slotIndex);
        if (itemsAccuallyMoved < draggedAmount)
            draggedSlot._inventory.AddItem(draggedItem, draggedAmount - itemsAccuallyMoved);*/
    }

    public void DeleteItem()
    {
        _inventory.RemoveItemFromIndex(_slotIndex);
    }
}

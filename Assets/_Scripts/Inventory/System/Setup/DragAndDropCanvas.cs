using UnityEngine;
using Utilities.Utilities.Core;

public class DragAndDropCanvas : Singleton<DragAndDropCanvas>
{
    [SerializeField] private InventorySlot _slot;
    
    public void Setup(InventoryItem item)
    {
        _slot.ResetSlot();
        _slot.SetupSlotUi(item.item, item.quantity);
    }

    public void ShowImage()
    {
        _slot.gameObject.SetActive(true);
    }

    public void HideImage()
    {
        _slot.gameObject.SetActive(false);
    }

    public void SetDraggingPosition(Vector2 position)
    {
        _slot.transform.position = position;
    }
}

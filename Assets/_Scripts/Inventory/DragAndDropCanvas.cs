using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCanvas : Singleton<DragAndDropCanvas>
{
    [SerializeField] private InventorySlot _slot;
    
    public void Setup(KeyValuePair<Item, int> item)
    {
        _slot.ResetSlot();
        _slot.SetupSlotUi(item.Key, item.Value);
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

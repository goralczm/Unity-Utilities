using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _amountText;

    private CanvasGroup _canvasGroup;
    private RectTransform _rect;
    private InventorySlot _slot;

    private DragAndDropCanvas _dragCanvas;

    private void Awake()
    {
        _dragCanvas = DragAndDropCanvas.Instance;
        if (!TryGetComponent(out _canvasGroup))
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _rect = GetComponent<RectTransform>();
        _slot = GetComponentInParent<InventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        DisableItemInfo();
        InventoryItem item = new InventoryItem(_slot.Item.item, _slot.Item.quantity);
        _dragCanvas.Setup(item);
        _dragCanvas.ShowImage();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        _dragCanvas.SetDraggingPosition(transform.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition = Vector2.zero;
        EnableItemInfo();
        _canvasGroup.blocksRaycasts = true;
        _dragCanvas.HideImage();
    }

    private void DisableItemInfo()
    {
        Color color = Color.white;
        color.a = 0;
        _icon.color = color;
        _amountText.SetActive(false);
    }

    private void EnableItemInfo()
    {
        _icon.color = Color.white;
        _amountText.SetActive(true);
    }
}

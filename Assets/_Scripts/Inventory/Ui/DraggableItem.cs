using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rect;
    private Image _image;

    private DragAndDropCanvas _dragCanvas;

    private void Awake()
    {
        _dragCanvas = DragAndDropCanvas.Instance;
        _canvasGroup = GetComponent<CanvasGroup>();
        _image = GetComponent<Image>();
        _rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _dragCanvas.SetImageAndSize(_image.sprite, _rect.rect.size);
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
        _canvasGroup.blocksRaycasts = true;
        _dragCanvas.HideImage();
    }
}

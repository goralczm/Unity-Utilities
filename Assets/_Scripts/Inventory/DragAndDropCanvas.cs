using UnityEngine;
using UnityEngine.UI;

public class DragAndDropCanvas : Singleton<DragAndDropCanvas>
{
    [SerializeField] private Image _draggingImage;
    [SerializeField] private RectTransform _dragRect;
    
    public void SetImageAndSize(Sprite newImage, Vector2 size)
    {
        _draggingImage.sprite = newImage;
        _dragRect.sizeDelta = size;
    }

    public void ShowImage()
    {
        _draggingImage.gameObject.SetActive(true);
    }

    public void HideImage()
    {
        _draggingImage.gameObject.SetActive(false);
    }

    public void SetDraggingPosition(Vector2 position)
    {
        _draggingImage.transform.position = position;
    }
}

using System;
using UnityEngine;
using Utilities.MapEditor;
using Utilities.MapEditor.Tiles;
using Utilities.Utilities.Input;
using Utilities.Utilities.Shapes;

public class RectangleSelection
{
    private Rectangle _rect;
    private Vector2 _startDrag;
    private Vector2 _startRectPos;
    private bool _isDragging;
    private bool _isSelecting;

    public Rectangle GetRect() => _rect;

    public RectangleSelection()
    {
        _rect = new GameObject("Selection", typeof(Rectangle)).GetComponent<Rectangle>();
    }

    public void BeginSelection()
    {
        _startDrag = MouseInput.MouseWorldPos;
    }

    public void OnMouseHold()
    {
        Vector2 dir = MouseInput.MouseWorldPos - _startDrag;

        if (!_isSelecting && !_isDragging && _rect.IsInside(MouseInput.MouseWorldPos))
        {
            _isDragging = true;
            _startRectPos = _rect.transform.position;
        }
        else
            _isSelecting = true;

        if (_isDragging)
            MoveSelection(dir);

        if (!_isDragging && _isSelecting)
        {
            MakeSelection(dir);
        }
    }

    private void MoveSelection(Vector2 dir)
    {
        _rect.transform.position = _startRectPos + dir;
    }

    private void MakeSelection(Vector2 dir)
    {
        _rect.SetWidth(Mathf.Abs(dir.x));
        _rect.SetHeight(Mathf.Abs(dir.y));
        _rect.transform.position = _startDrag + dir / 2f;
    }

    public void OnMouseRelease()
    {
        _isDragging = false;
        _isSelecting = false;
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(_rect.gameObject);
    }
}

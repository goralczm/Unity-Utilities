using UnityEngine;

public class DragCameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("0 - Left Mouse Button\n1 - Right Mouse Button\n2 - Middle Mouse Button")]
    [SerializeField] private int _dragMouseButton = 0;

    private Vector2 _startDragPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_dragMouseButton))
        {
            _startDragPos = MouseInput.MouseWorldPos;
            return;
        }

        if (!Input.GetMouseButton(_dragMouseButton))
            return;

        Vector2 offset = MouseInput.MouseWorldPos - (Vector2)transform.position;
        Vector2 dir = _startDragPos - offset;
        Vector3 newPos = new Vector3(dir.x, dir.y, -10);

        transform.position = newPos;
    }

    private void OnValidate()
    {
        _dragMouseButton = Mathf.Clamp(_dragMouseButton, 0, 2);
    }
}

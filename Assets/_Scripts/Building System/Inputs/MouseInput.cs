using UnityEngine;

public static class MouseInput
{
    public static Vector2 MouseWorldPos
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;

            return _cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public static float ScrollWheel => Input.GetAxis("Mouse ScrollWheel");

    private static Camera _cam;
}

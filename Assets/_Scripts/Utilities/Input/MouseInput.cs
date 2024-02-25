using UnityEngine;

namespace Utilities.Utilities.Input
{
    public static class MouseInput
    {
        public static Vector2 MouseWorldPos
        {
            get
            {
                if (_cam == null)
                    _cam = Camera.main;

                return _cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            }
        }

        public static float ScrollWheel => UnityEngine.Input.GetAxis("Mouse ScrollWheel");

        private static Camera _cam;
    }
}

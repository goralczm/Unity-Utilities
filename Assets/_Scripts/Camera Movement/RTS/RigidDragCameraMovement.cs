using UnityEngine;
using Utilities.Utilities.Input;
using Utilities.Utilities.Shapes;

namespace Utilities.CameraMovement
{
    /// <summary>
    /// Rigidly drags the camera based on the mouse input.
    /// </summary>
    public class RigidDragCameraMovement : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("0 - Left Mouse Button\n1 - Right Mouse Button\n2 - Middle Mouse Button")]
        [SerializeField] private int _dragMouseButton = 0;

        [Header("Optional")]
        [SerializeField] private GameObject _boundryTransform;

        private IShape _boundry;
        private Vector2 _startDragPos;

        private void Awake()
        {
            if (_boundryTransform != null)
                _boundry = _boundryTransform.GetComponent<IShape>();
        }

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
            if (_boundry != null)
                dir = _boundry.ClampPositionInside(dir);
            Vector3 newPos = new Vector3(dir.x, dir.y, -10);

            transform.position = newPos;
        }

        private void OnValidate()
        {
            _dragMouseButton = Mathf.Clamp(_dragMouseButton, 0, 2);
        }
    }
}

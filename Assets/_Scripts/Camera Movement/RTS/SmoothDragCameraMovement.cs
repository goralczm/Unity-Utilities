using UnityEngine;
using Utilities.Utilities.Shapes;

namespace Utilities.CameraMovement
{
    /// <summary>
    /// Smoothly drags the camera based on the mouse input.
    /// </summary>
    public class SmoothDragCameraMovement : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("0 - Left Mouse Button\n1 - Right Mouse Button\n2 - Middle Mouse Button")]
        [SerializeField] private int _dragMouseButton = 0;
        [SerializeField, Range(.1f, 2f)] private float _smoothness = .15f;
        [SerializeField, Range(.25f, 4f)] private float _dragForce = 2f;

        [Header("Optional")]
        [SerializeField] private GameObject _boundryTransform;

        private IShape _boundry;
        private Vector3 _startMouseDragPos;
        private Vector3 _startCameraDragPos;

        private Vector3 _targetPos;

        public void SetDragForce(float dragForce) => _dragForce = dragForce;

        private void Awake()
        {
            _boundry = _boundryTransform.GetComponent<IShape>();

            _targetPos = Vector3.zero;
            _targetPos.z = -10;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * (1 / _smoothness));

            if (UnityEngine.Input.GetMouseButtonDown(_dragMouseButton))
            {
                _startMouseDragPos = UnityEngine.Input.mousePosition;
                _startCameraDragPos = transform.position;
                return;
            }

            if (!UnityEngine.Input.GetMouseButton(_dragMouseButton))
                return;

            Vector3 diff = UnityEngine.Input.mousePosition - _startMouseDragPos;
            Vector3 finalPos = _startCameraDragPos - diff / (64f / _dragForce);

            if (_boundry != null)
                finalPos = _boundry.ClampPositionInside(finalPos);
            finalPos.z = -10;

            _targetPos = finalPos;
        }

        private void OnValidate()
        {
            _dragMouseButton = Mathf.Clamp(_dragMouseButton, 0, 2);
        }
    }
}

using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float _minZoom = 2f, _maxZoom = 10f;
    
    private Camera _cam;
    
    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        float newCameraSize = _cam.orthographicSize - MouseInput.ScrollWheel * 10f;
        newCameraSize = Mathf.Clamp(newCameraSize, _minZoom, _maxZoom);
        _cam.orthographicSize = newCameraSize;
    }
}

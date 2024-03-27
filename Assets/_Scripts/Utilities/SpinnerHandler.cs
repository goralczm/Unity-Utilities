using UnityEngine;

namespace Utilities.Utilities
{
    public class SpinnerHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _radius = 3f;
        [SerializeField] private bool _inverted;

        private Spinner _spinner;

        private void Start()
        {
            _spinner = new Spinner(transform.childCount, _speed, _radius, _inverted);
        }

        private void Update()
        {
            _spinner.UpdatePositions();
            Vector2[] positions = _spinner.GetAllPointsPositions(transform.position);

            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 dampedPosition = Vector2.Lerp(transform.GetChild(i).transform.position, positions[i], Time.deltaTime * _speed);
                transform.GetChild(i).transform.position = dampedPosition;
            }
        }

        private void OnValidate()
        {
            if (_speed < 0)
                _speed = 0;

            if (_radius < 0)
                _radius = 0;

            if (_spinner != null)
            {
                _spinner.SetSpeed(_speed);
                _spinner.SetRadius(_radius);
                _spinner.SetInverted(_inverted);
            }
        }
    }
}

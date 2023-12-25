using TMPro;
using UnityEngine;

public class GradualIntAnimation : MonoBehaviour
{
    [SerializeField] private int _value;

    [Header("Settings")]
    [SerializeField] private float _changeIntervals = .03f;

    [Header("Instances")]
    [SerializeField] private TextMeshProUGUI _text;

    private int _currentVisualValue;
    private float _timer;

    private void Update()
    {
        if (_timer <= 0)
        {
            if (_currentVisualValue != _value)
            {
                int diff = Mathf.Abs(_value - _currentVisualValue);

                int decimalPlaces = -1;
                while (diff > 0)
                {
                    diff /= 10;
                    decimalPlaces++;
                }

                if (_value > _currentVisualValue)
                    _currentVisualValue += (int)Mathf.Pow(10, decimalPlaces);
                else
                    _currentVisualValue -= (int)Mathf.Pow(10, decimalPlaces);

                _text.SetText(_currentVisualValue.ToString());
            }
            _timer = _changeIntervals;
        }
        else
            _timer -= Time.deltaTime;
    }
}

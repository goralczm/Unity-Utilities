using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : SettingsInput
{
    [Header("Settings")]
    [SerializeField] private float _defaultValue;

    [Header("Instances")]
    [SerializeField] private Slider _slider;

    public override void ResetToDefault()
    {
        _slider.value = _defaultValue;
    }

    public override object Save()
    {
        return _slider.value;
    }

    public override void Load(object data)
    {
        _slider.value = (float)data;
    }
}

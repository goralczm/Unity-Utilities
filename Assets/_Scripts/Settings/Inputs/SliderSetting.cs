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

    protected override void SaveData(SettingsInputData data)
    {
        data.SaveData("Value", _slider.value);
    }

    public override void Load(SettingsInputData data)
    {
        _slider.value = (float)data.GetData("Value");
    }
}

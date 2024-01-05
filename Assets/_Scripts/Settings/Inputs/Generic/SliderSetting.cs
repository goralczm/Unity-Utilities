using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : SettingsInput
{
    [Header("Settings")]
    [SerializeField] private float _defaultValue;

    [Header("Instances")]
    [SerializeField] protected Slider _slider;

    public override void RevertLast()
    {
        base.RevertLast();
        _slider.value = (float)_valueHistory.Pop();
    }

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

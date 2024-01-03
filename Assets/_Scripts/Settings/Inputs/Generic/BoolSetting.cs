using UnityEngine;
using UnityEngine.UI;

public class BoolSetting : SettingsInput
{
    [Header("Settings")]
    [SerializeField] private bool _defaultValue;

    [Header("Instances")]
    [SerializeField] protected Toggle _toggle;

    public override void ResetToDefault()
    {
        _toggle.isOn = _defaultValue;
    }

    public override object Save()
    {
        return _toggle.isOn;
    }

    public override void Load(object data)
    {
        _toggle.isOn = (bool)data;
    }
}

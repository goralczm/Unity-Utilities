using UnityEngine;
using UnityEngine.UI;

public class BoolSetting : SettingsInput
{
    [Header("Settings")]
    [SerializeField] private bool _defaultValue;

    [Header("Instances")]
    [SerializeField] private Toggle _toggle;

    public override void ResetToDefault()
    {
        _toggle.isOn = _defaultValue;
    }

    protected override void SaveData(SettingsInputData data)
    {
        data.SaveData("Value", _toggle.isOn);
    }

    public override void Load(SettingsInputData data)
    {
        _toggle.isOn = (bool)data.GetData("Value");
    }
}

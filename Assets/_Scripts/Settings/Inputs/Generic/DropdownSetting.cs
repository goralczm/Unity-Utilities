using UnityEngine;
using TMPro;

public class DropdownSetting : SettingsInput
{
    [Header("Settings")]
    [SerializeField] protected int _defaultValue;

    [Header("Instances")]
    [SerializeField] protected TMP_Dropdown _dropdown;

    public override void ResetToDefault()
    {
        _dropdown.value = _defaultValue;
    }

    public override object Save()
    {
        return _dropdown.value;
    }

    public override void Load(object data)
    {
        _dropdown.value = (int)data;
    }
}

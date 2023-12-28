using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private Transform _settingInputsParent;

    private Dictionary<string, SettingsInput> _settings = new Dictionary<string, SettingsInput>();

    private void Start()
    {
        AddSettingsInput();
        ResetToDefaults();
        LoadSettings();
    }

    private void AddSettingsInput()
    {
        _settings.Clear();
        for (int i = 0; i < _settingInputsParent.childCount; i++)
        {
            SettingsInput input = _settingInputsParent.GetChild(i).GetComponent<SettingsInput>();
            if (_settings.ContainsKey(input.name))
                throw new System.Exception($"Duplicate settings name found! {input.name}");

            _settings.Add(input.SettingName, input);
        }
    }

    [ContextMenu("Reset Settings")]
    public void ResetToDefaults()
    {
        foreach (var input in _settings)
            input.Value.ResetToDefault();
    }

    [ContextMenu("Save Settings")]
    public void SaveSettings()
    {
        SaveableData settings = new SaveableData();
        foreach (var input in _settings)
            settings.SaveData(input.Key, input.Value.Save());

        SaveSystem.SaveData(settings, "Settings");
    }

    [ContextMenu("Load Settings")]
    private void LoadSettings()
    {
        SaveableData data = SaveSystem.LoadData("Settings") as SaveableData;
        if (data == null)
            return;

        foreach (var savedObj in data.savedDatas)
        {
            SettingsInput setting = GetSettingsInput(savedObj.Key);
            if (setting == null)
                continue;

            setting.Load(savedObj.Value);
        }
    }

    private SettingsInput GetSettingsInput(string name)
    {
        if (!_settings.ContainsKey(name))
            return null;

        return _settings[name];
    }
}

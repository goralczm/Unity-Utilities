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
        SettingsInputData[] settingsInputs = new SettingsInputData[_settings.Count];
        int index = 0;
        foreach (var input in _settings)
        {
            settingsInputs[index] = input.Value.Save();
            index++;
        }

        SettingsData data = new SettingsData(settingsInputs);
        SaveSystem.SaveData(data, "Settings");
    }

    [ContextMenu("Load Settings")]
    private void LoadSettings()
    {
        SettingsData data = SaveSystem.LoadData("Settings") as SettingsData;
        if (data == null)
            return;

        foreach (SettingsInputData settingsInput in data.settingsInputs)
        {
            SettingsInput input = GetSettingsInput(settingsInput.settingName);
            if (input == null)
                continue;

            input.Load(settingsInput);
        }
    }

    private SettingsInput GetSettingsInput(string name)
    {
        if (!_settings.ContainsKey(name))
            return null;

        return _settings[name];
    }
}

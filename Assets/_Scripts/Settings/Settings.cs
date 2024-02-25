using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private UnityEvent _onSetupDone;

    [Header("Instances")]
    [SerializeField] private Transform _settingInputsParent;
    [SerializeField] private ConfirmationPopup _confirmation;
    [SerializeField] private SettingsTooltip _tooltip;

    private Dictionary<string, SettingsInput> _settings = new Dictionary<string, SettingsInput>();

    private void Start()
    {
        AddSettingsInput();
        ResetToDefaults();
        LoadSettings();

        _onSetupDone?.Invoke();
    }

    private void AddSettingsInput()
    {
        _settings.Clear();

        SettingsInput[] foundInputs = GetComponentsInChildren<SettingsInput>();
        for (int i = 0; i < foundInputs.Length; i++)
        {
            SettingsInput input = foundInputs[i];

            if (_settings.ContainsKey(input.name))
                throw new System.Exception($"Duplicate settings name found! {input.name}");

            input.SetSettings(this);
            input.Setup();
            _settings.Add(input.SettingName, input);
        }
    }

    public void ResetToDefaults()
    {
        foreach (var input in _settings)
        {
            input.Value.ResetToDefault();
            ConfirmSetting();
        }
    }

    public void LoadSettings()
    {
        SaveableData data = SaveSystem.LoadData("Settings") as SaveableData;
        if (data == null)
            return;

        ResetToDefaults();

        foreach (var savedObj in data.savedDatas)
        {
            SettingsInput setting = GetSettingsInput(savedObj.Key);
            if (setting == null)
                continue;

            setting.Load(savedObj.Value);
            ConfirmSetting();
        }
    }


    public void SaveSettings()
    {
        SaveableData settings = new SaveableData();
        foreach (var input in _settings)
            settings.SaveData(input.Key, input.Value.Save());

        SaveSystem.SaveData(settings, "Settings");
    }


    private SettingsInput GetSettingsInput(string name)
    {
        if (!_settings.ContainsKey(name))
            return null;

        return _settings[name];
    }

    public void ShowConfirmation(SettingsInput input)
    {
        _confirmation.Show(input);
    }

    public void ConfirmSetting()
    {
        _confirmation.ForceConfirm();
    }

    public void ShowTooltip(SettingsInput input)
    {
        _tooltip.SetCover(input.GetTooltipCoverSprite());
        _tooltip.SetSettingName(input.name);
        _tooltip.SetDescription(input.GetTooltipDescription());
    }
}

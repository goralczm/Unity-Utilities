using UnityEngine;

public abstract class SettingsInput : MonoBehaviour
{
    [field: SerializeField] public string SettingName { get; protected set; }

    public abstract void ResetToDefault();
    public abstract void Load(SettingsInputData data);
    protected abstract void SaveData(SettingsInputData data);
    public SettingsInputData Save()
    {
        SettingsInputData newSetting = new SettingsInputData(SettingName);
        SaveData(newSetting);

        return newSetting;
    }
}

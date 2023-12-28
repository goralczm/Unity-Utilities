[System.Serializable]
public class SettingsData
{
    public SettingsInputData[] settingsInputs;

    public SettingsData(SettingsInputData[] settingsInputs)
    {
        this.settingsInputs = settingsInputs;
    }
}

[System.Serializable]
public class SettingsInputData : SavableData
{
    public string settingName;

    public SettingsInputData(string settingName) : base()
    {
        this.settingName = settingName;
    }
}

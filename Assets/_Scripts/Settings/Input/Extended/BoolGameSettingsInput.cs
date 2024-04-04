using UnityEngine;
using Utilities.Settings.Input.Basic;

namespace Utilities.Settings.Input.Extended
{
    public class BoolGameSettingsInput : BoolSetting
    {
        public void ToggleSetting(bool state)
        {
            GameSettings.SaveSetting(SettingName, state);

            OnValueChanged();
        }
    }
}

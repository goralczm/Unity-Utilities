using UnityEngine;
using Utilities.Settings.Input.Basic;

namespace Utilities.Settings.Input.Extended
{
    public class FullscreenInput : BoolSetting
    {
        public void ToggleFullscreen(bool state)
        {
            Screen.fullScreen = state;

            OnValueChanged();
        }
    }
}

using UnityEngine;

public class FullscreenInput : BoolSetting
{
    public void ToggleFullscreen(bool state)
    {
        Screen.fullScreen = state;

        OnValueChanged();
    }
}

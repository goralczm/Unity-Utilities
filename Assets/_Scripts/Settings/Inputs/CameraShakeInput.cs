public class CameraShakeInput : BoolSetting
{
    public void OnValueChanged(bool isOn)
    {
        GameSettings.CAMERA_SHAKE = isOn;
    }
}

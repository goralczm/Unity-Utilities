using UnityEngine;
using UnityEngine.Audio;

public class VolumeInput : SliderSetting
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _exposedVolumeName;

    public void SetVolume(float value)
    {
        _mixer.SetFloat(_exposedVolumeName, value);
    }
}

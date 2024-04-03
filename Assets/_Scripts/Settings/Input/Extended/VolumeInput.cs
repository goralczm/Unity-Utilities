using UnityEngine;
using UnityEngine.Audio;
using Utilities.Settings.Input.Basic;

namespace Utilities.Settings.Input.Extended
{
    public class VolumeInput : SliderSetting
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _exposedVolumeName;

        public override void PreviousOption()
        {
            _slider.value -= 4;
        }

        public override void NextOption()
        {
            _slider.value += 4;
        }

        public void SetVolume(float value)
        {
            _mixer.SetFloat(_exposedVolumeName, value);

            OnValueChanged();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Utilities.Utilities.UI.Texts;

namespace Utilities.Settings.UI
{
    public class SliderValueText : ValueText
    {
        [SerializeField] private Slider _slider;

        public override void SetIntValue(float value)
        {
            value -= _slider.minValue;
            value = value / (_slider.maxValue - _slider.minValue);
            value *= 100;
            base.SetIntValue((int)value);
        }
    }
}

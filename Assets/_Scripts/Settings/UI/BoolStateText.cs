using TMPro;
using UnityEngine;

namespace Utilities.Settings.UI
{
    public class BoolStateText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateText(bool isOn)
        {
            if (isOn)
                _text.SetText("ON");
            else
                _text.SetText("OFF");
        }
    }
}

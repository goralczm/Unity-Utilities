using UnityEngine;
using Utilities.Settings.Input.Basic;
using Utilities.Utilities.UI;
using Utilities.Utilities.UI.Texts;

namespace Utilities.Settings.UI
{
    public class ConfirmationPopup : MonoBehaviour
    {
        [SerializeField] private UITweener _tweener;
        [SerializeField] private ValueText _valueText;

        private const float TIME_TO_REVERT = 15f;

        private SettingsInput _input;
        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;
            _valueText.SetIntValue(_timer);

            if (_timer <= 0)
                Revert();
        }

        public void Show(SettingsInput input)
        {
            _input = input;
            _timer = TIME_TO_REVERT;
            _tweener.Show();
        }

        public void Revert()
        {
            if (_input == null)
                return;

            _input.RevertLast();
            _input = null;
        }

        public void ConfirmSetting()
        {
            _timer = TIME_TO_REVERT;
            _tweener.Hide();
        }

        public void ForceConfirm()
        {
            _timer = TIME_TO_REVERT;
            _tweener.Hide();
            _tweener.gameObject.SetActive(false);
        }
    }
}

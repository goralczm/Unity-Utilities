using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Utilities.UI
{
    public class UITweenerGroup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _autoSetup;

        [Header("Instances")]
        [SerializeField] private UITweener[] _tweeners;

        private void Awake()
        {
            if (_autoSetup)
                _tweeners = GetComponentsInChildren<UITweener>(true);
        }

        public void ShowAll()
        {
            for (int i = 0; i < _tweeners.Length; i++)
                _tweeners[i].Show();
        }

        public void HideAll()
        {
            for (int i = 0; i < _tweeners.Length; i++)
                _tweeners[i].Hide();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header;
        [TextArea(7, 7)] public string content;

        private const float DELAY = .5f;

        private float _delayTimer;
        private bool _isWaiting;

        public virtual void Update()
        {
            if (_isWaiting)
            {
                _delayTimer -= Time.deltaTime;
                if (_delayTimer <= 0)
                {
                    TooltipSystem.Show(content, header);
                    _isWaiting = false;
                }
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            ShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltip();
        }

        public void OnMouseEnter()
        {
            ShowTooltip();
        }

        private void OnMouseExit()
        {
            HideTooltip();
        }

        private void ShowTooltip()
        {
            _delayTimer = DELAY;
            _isWaiting = true;
        }

        private void HideTooltip()
        {
            _isWaiting = false;
            TooltipSystem.Hide();
        }
    }
}

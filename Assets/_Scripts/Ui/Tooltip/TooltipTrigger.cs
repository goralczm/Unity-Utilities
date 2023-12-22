using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        _delayTimer = DELAY;
        _isWaiting = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isWaiting = false;
        TooltipSystem.Hide();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        _isWaiting = false;
        TooltipSystem.Hide();
    }
}

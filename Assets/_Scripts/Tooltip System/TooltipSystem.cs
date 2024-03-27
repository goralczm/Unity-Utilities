using UnityEngine;
using Utilities.Utilities.Core;
using Utilities.Utilities.UI;

public class TooltipSystem : Singleton<TooltipSystem>
{
    [SerializeField] private Tooltip _tooltip;
    [SerializeField] private UITweener _tweener;

    public static void Show(string content, string header = "")
    {
        if (Input.GetMouseButton(0))
            return;

        Instance._tooltip.SetText(content, header);
        Instance._tweener.Show();
    }

    public static void Hide()
    {
        Instance._tweener.Hide();
    }
}

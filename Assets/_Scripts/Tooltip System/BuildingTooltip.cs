using UnityEngine;

public class BuildingTooltip : TooltipTrigger
{
    private void Start()
    {
        header = "";
        content = transform.name;
    }
}

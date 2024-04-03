namespace Utilities.TooltipSystem
{
    public class ObjectNameTooltip : TooltipTrigger
    {
        private void Start()
        {
            header = "";
            content = transform.name;
        }
    }
}

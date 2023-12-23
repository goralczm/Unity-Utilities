public class InventorySlotTooltip : TooltipTrigger
{
    private InventorySlot _slot;

    private void Start()
    {
        _slot = GetComponent<InventorySlot>();
        _slot.OnSlotChangedHandler += UpdateTooltipInfo;
    }

    public override void Update()
    {
        if (_slot.Item == null)
            return;

        base.Update();
    }

    public void UpdateTooltipInfo()
    {
        header = _slot.Item.item.name;
        content = _slot.Item.item.description;
    }
}

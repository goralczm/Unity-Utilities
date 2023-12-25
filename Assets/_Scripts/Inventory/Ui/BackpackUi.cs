using UnityEngine;

public class BackpackUi : InventoryUi
{
    [SerializeField] private UITweener _tweener;

    public void ShowBackpack(Backpack backpack)
    {
        if (backpack == inventory)
        {
            HideBackpack();
            return;
        }

        SetNewInventory(backpack);
        _tweener.Show();
    }

    public void HideBackpack()
    {
        _tweener.Hide();
    }

    public void ResetInventory()
    {
        inventory = null;
    }
}

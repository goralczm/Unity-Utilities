using UnityEngine;

public class BackpackUi : InventoryUi
{
    [SerializeField] private UITweener _tweener;

    #region Singleton
    public static BackpackUi Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void ShowBackpack(Backpack backpack)
    {
        if (backpack == _inventory)
        {
            HideBackpack();
            return;
        }

        SetNewInventory(backpack);
        _tweener.Show();
    }

    public void HideBackpack()
    {
        _inventory = null;
        _tweener.Hide();
    }
}

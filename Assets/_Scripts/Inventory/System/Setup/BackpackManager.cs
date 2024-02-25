using UnityEngine;
using Utilities.Utilities.Core;

public class BackpackManager : Singleton<BackpackManager>
{
    [Header("Instances")]
    [SerializeField] private BackpackUi _backpackUi;
    [SerializeField] private ProximityInteractionHandler _playerInteraction;

    public Inventory CurrentInventory => _backpackUi.inventory;

    public void ShowBackpack(Backpack newBackpack)
    {
        _backpackUi.ShowBackpack(newBackpack);
    }

    public void HideBackpack()
    {
        _backpackUi.HideBackpack();
    }
}

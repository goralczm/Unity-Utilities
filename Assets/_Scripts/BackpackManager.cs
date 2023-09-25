using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : Singleton<BackpackManager>
{
    [Header("Instances")]
    [SerializeField] private BackpackUi _backpackUi;
    [SerializeField] private PlayerInteract _playerInteraction;

    public Inventory CurrentInventory => _backpackUi.inventory;

    private void Update()
    {
        CloseBackpackIfPlayerIsTooFar();
    }

    public void ShowBackpack(Backpack newBackpack)
    {
        _backpackUi.ShowBackpack(newBackpack);
    }

    public void HideBackpack()
    {
        _backpackUi.HideBackpack();
    }

    public void CloseBackpackIfPlayerIsTooFar()
    {
        if (_backpackUi.inventory == null)
            return;

        float distance = Vector2.Distance(_playerInteraction.transform.position, _backpackUi.inventory.transform.position);
        if (distance > _playerInteraction.InteractionRadius)
            HideBackpack();
    }
}

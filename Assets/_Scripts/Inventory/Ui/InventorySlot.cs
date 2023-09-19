using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;

    private KeyValuePair<Item, int> _item;

    public void SetupSlotUi(Item newItem, int amount)
    {
        _item = new KeyValuePair<Item, int>(newItem, amount);

        _amountText.SetText(_item.Value.ToString());
        _icon.sprite = _item.Key.icon;
        _icon.enabled = true;
    }

    public void ResetSlot()
    {
        _amountText.SetText("");
        _icon.sprite = null;
        _icon.enabled = false;
    }
}

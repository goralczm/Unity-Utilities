using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode[] _hotbarKeys;

    [Header("Instances")]
    [SerializeField] private UITweener _backpackUi;
    [SerializeField] private InventorySlot[] _hotbarSlots;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            _backpackUi.Toggle();

        for (int i = 0; i < _hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(_hotbarKeys[i]))
                _hotbarSlots[i].UseItem();
        }
    }
}

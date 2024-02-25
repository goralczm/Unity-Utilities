using UnityEngine;
using Utilities.Utilities.Core;

public class PlayerInventoryManager : Singleton<PlayerInventoryManager>
{
    [field: SerializeField] public Inventory PlayerInventory { get; private set; }
}

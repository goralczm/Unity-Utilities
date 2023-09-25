using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public Inventory PlayerInventory { get; private set; }
}

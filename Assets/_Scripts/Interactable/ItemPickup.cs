using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Item Item { get; private set; }

    public void Interact()
    {
        GameManager.Instance.PlayerInventory.AddItem(Item, 1);
        Destroy(gameObject);
    }
}

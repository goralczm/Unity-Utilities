using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item;

    public void Interact()
    {
        GameManager.Instance.PlayerInventory.AddItem(_item, 1);
        Destroy(gameObject);
    }
}

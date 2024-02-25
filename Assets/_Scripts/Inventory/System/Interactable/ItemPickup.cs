using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item;

    private void Start()
    {
        if (_item == null)
            return;

        Setup(_item);
    }

    public void Setup(Item item)
    {
        _item = item;
        GetComponent<SpriteRenderer>().sprite = _item.icon;
    }

    public void Interact(GameObject requester)
    {
        requester.GetComponent<Inventory>().AddItem(_item, 1);
        Destroy(gameObject);
    }

    public void OutOfRange()
    {
        
    }

    public Item GetItem()
    {
        return _item;
    }
}

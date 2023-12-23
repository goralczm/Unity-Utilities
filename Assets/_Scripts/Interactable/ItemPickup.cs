using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] public Item _item;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _item.icon;
    }

    public void Interact(GameObject requester)
    {
        requester.GetComponent<Inventory>().AddItem(_item, 1);
        gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        
    }
}

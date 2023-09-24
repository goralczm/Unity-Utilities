using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/New Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public string description = "Description not set yet.";
    public Sprite icon;

    public int stackSize = 16;

    public virtual void UseItem()
    {

    }
}

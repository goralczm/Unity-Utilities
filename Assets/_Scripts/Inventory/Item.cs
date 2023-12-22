using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/New Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public int id;
    [TextArea(5, 5)] public string description = "Description not set yet.";
    public Sprite icon;

    public int stackSize = 16;

    public virtual void UseItem()
    {

    }
}

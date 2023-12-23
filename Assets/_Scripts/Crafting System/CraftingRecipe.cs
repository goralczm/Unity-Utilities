using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/New Crafting Recipe", fileName = "New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<InventoryItem> components;
    public List<InventoryItem> outputs;
}

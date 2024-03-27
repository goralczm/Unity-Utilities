using System.Collections.Generic;
using UnityEngine;

namespace Utilities.CraftingSystem
{
    [CreateAssetMenu(menuName = "Inventory/New Crafting Recipe", fileName = "New Crafting Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        public List<InventoryItem> components;
        public List<InventoryItem> outputs;
    }
}

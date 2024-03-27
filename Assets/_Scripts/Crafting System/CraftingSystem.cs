using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.CraftingSystem
{
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        /// <summary>
        /// Checks if inventory has enough ingredients and free space to craft the recipe. If yes, then it crafts it.
        /// </summary>
        /// <param name="recipe">The recipe to be crafted.</param>
        public void TryCraftRecipe(CraftingRecipe recipe)
        {
            if (CanCraft(recipe))
                CraftRecipe(recipe);
        }

        /// <summary>
        /// Checks if inventory has enough ingredients and free space to craft the recipe.
        /// </summary>
        /// <param name="recipe">The recipe to be crafted.</param>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        public bool CanCraft(CraftingRecipe recipe)
        {
            return HasComponents(recipe) && HasSpaceForOutput(recipe);
        }

        /// <summary>
        /// Checks if inventory has enough ingredients to creaft the recipe.
        /// </summary>
        /// <param name="recipe">The recipe to be crafted.</param>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool HasComponents(CraftingRecipe recipe)
        {
            Dictionary<Item, int> componentsLeft = recipe.components.ToDictionary(k => k.item, v => v.quantity);

            foreach (InventoryItem item in _inventory.items)
            {
                if (item.item == null)
                    continue;

                if (componentsLeft.ContainsKey(item.item))
                {
                    componentsLeft[item.item] -= item.quantity;
                    if (componentsLeft[item.item] <= 0)
                        componentsLeft.Remove(item.item);
                }

                if (componentsLeft.Count == 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if inventory has enough free space for recipe outputs after crafting.
        /// </summary>
        /// <param name="recipe">The recipe to be crafted.</param>
        /// <returns>The <see cref="bool"/> state of the condition.</returns>
        private bool HasSpaceForOutput(CraftingRecipe recipe)
        {
            List<InventoryItem> itemsCopy = new List<InventoryItem>(_inventory.items);
            foreach (InventoryItem component in recipe.components)
                _inventory.RemoveItem(component.item, component.quantity);

            foreach (InventoryItem output in recipe.outputs)
            {
                if (!_inventory.CanAdd(output.item, output.quantity))
                {
                    _inventory.items = itemsCopy;
                    _inventory.InvokeOnItemChangedHandler();
                    return false;
                }

                _inventory.AddItem(output.item, output.quantity);
            }

            _inventory.items = itemsCopy;
            _inventory.InvokeOnItemChangedHandler();
            return true;
        }

        /// <summary>
        /// Proceeds the crafting.
        /// </summary>
        /// <param name="recipe">The recipe to be crafted.</param>
        public void CraftRecipe(CraftingRecipe recipe)
        {
            foreach (InventoryItem component in recipe.components)
                _inventory.RemoveItem(component.item, component.quantity);

            foreach (InventoryItem output in recipe.outputs)
                _inventory.AddItem(output.item, output.quantity);
        }
    }
}
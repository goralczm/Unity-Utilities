using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public void TryCraftRecipe(CraftingRecipe recipe)
    {
        if (CanCraft(recipe))
            CraftRecipe(recipe);
    }

    private bool CanCraft(CraftingRecipe recipe)
    {
        return HasComponents(recipe) && HasSpaceForOutput(recipe);
    }

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

    public void CraftRecipe(CraftingRecipe recipe)
    {
        foreach (InventoryItem component in recipe.components)
            _inventory.RemoveItem(component.item, component.quantity);

        foreach (InventoryItem output in recipe.outputs)
            _inventory.AddItem(output.item, output.quantity);
    }
}

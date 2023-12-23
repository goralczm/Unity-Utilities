using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public void CraftRecipe(CraftingRecipe recipe)
    {
        foreach (InventoryItem component in recipe.components)
            _inventory.RemoveItem(component.item, component.quantity);

        foreach (InventoryItem output in recipe.outputs)
            _inventory.AddItem(output.item, output.quantity);
    }
}

using UnityEngine;

namespace Utilities.CraftingSystem.UI
{
    /// <summary>
    /// Displays the crafting recipe and serves a functionality of crafting recipes using <see cref="CraftingSystem"/>.
    /// </summary>
    public class RecipeButton : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private CraftingRecipe _recipe;

        [Header("Instances")]
        [SerializeField] private CraftingSystem _craftingSystem;
        [SerializeField] private InventorySlot _slotPrefab;
        [SerializeField] private GameObject _separatorPrefab;

        private void Start()
        {
            Setup(_recipe);
        }

        /// <summary>
        /// Setups display of the crafting recipe.
        /// </summary>
        /// <param name="recipe">The recipe to be displayed and cached.</param>
        public void Setup(CraftingRecipe recipe)
        {
            _recipe = recipe;
            foreach (InventoryItem component in recipe.components)
                Instantiate(_slotPrefab, transform).SetupSlotUi(component.item, component.quantity);

            Instantiate(_separatorPrefab, transform);

            foreach (InventoryItem output in recipe.outputs)
                Instantiate(_slotPrefab, transform).SetupSlotUi(output.item, output.quantity);

            transform.GetChild(0).SetAsLastSibling();
        }

        /// <summary>
        /// Tries to craft the cached recipe.
        /// </summary>
        public void Craft()
        {
            _craftingSystem.TryCraftRecipe(_recipe);
        }
    }
}

using UnityEngine;

namespace Utilities.CraftingSystem.UI
{
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

        public void Craft()
        {
            _craftingSystem.TryCraftRecipe(_recipe);
        }
    }
}

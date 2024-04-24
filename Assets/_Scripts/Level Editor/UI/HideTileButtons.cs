using UnityEngine;
using Utilities.Utilities.UI;

namespace Utilities.LevelEditor
{
    public class HideTileButtons : MonoBehaviour
    {
        [SerializeField] private UITweenerGroup _tweens;
        [SerializeField] private LevelEditor _editor;

        private void OnEnable()
        {
            _editor.OnToolChanged += HidePanelsOnSelectionTool;
        }

        private void OnDisable()
        {
            _editor.OnToolChanged -= HidePanelsOnSelectionTool;
        }

        private void HidePanelsOnSelectionTool(ToolType toolType)
        {
            if (toolType != ToolType.Selection)
                return;

            _tweens.HideAll();
        }
    }
}

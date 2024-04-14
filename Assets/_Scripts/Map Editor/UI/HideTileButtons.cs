using UnityEngine;
using Utilities.Utilities.UI;

namespace Utilities.MapEditor
{
    public class HideTileButtons : MonoBehaviour
    {
        [SerializeField] private UITweener[] _tileButtonsPanels;
        [SerializeField] private MapEditor _editor;

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

            HideAllPanels();
        }

        public void HideAllPanels()
        {
            for (int i = 0; i < _tileButtonsPanels.Length; i++)
                _tileButtonsPanels[i].Hide();
        }
    }
}

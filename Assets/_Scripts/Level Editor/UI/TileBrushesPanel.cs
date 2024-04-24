using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.LevelEditor.UI;

namespace Utilities.LevelEditor
{
    public class TileBrushesPanel : MonoBehaviour
    {
        private List<TileButton> _brushes = new List<TileButton>();

        private bool _initialized;

        private void Initialize()
        {
            if (_initialized)
                return;

            _brushes = GetComponentsInChildren<TileButton>().ToList();
            _initialized = true;
        }

        public void PaintFirstBrush()
        {
            Initialize();

            LevelEditor.Instance.BeginPaint(_brushes[0].GetBrush());
        }
    }
}

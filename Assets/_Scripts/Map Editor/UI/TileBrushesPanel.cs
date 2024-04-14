using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.MapEditor.UI;

namespace Utilities.MapEditor
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

            MapEditor.Instance.BeginPaint(_brushes[0].GetBrush());
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Utilities.LevelEditor.Tiles;

namespace Utilities.LevelEditor.UI
{
    public class TileButton : MonoBehaviour
    {
        [Header("Instances")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;

        private TileBrush _brush;
        
        public void Setup(TileBrush tile)
        {
            _brush = tile;

            _icon.sprite = tile.sprite;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(delegate {
                LevelEditor.Instance.BeginPaint(tile);
            });
        }

        public TileBrush GetBrush()
        {
            return _brush;
        }
    }
}

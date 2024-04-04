using UnityEngine;
using UnityEngine.UI;
using Utilities.MapEditor.Tiles;

namespace Utilities.MapEditor.UI
{
    public class TileButton : MonoBehaviour
    {
        [SerializeField] private TileBrush _tile;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            GetComponent<Image>().sprite = _tile.sprite;

            _button.onClick.AddListener(delegate {
                MapEditor.Instance.GetBuildingSystem().StartBuilding(_tile);
            });
        }
    }
}

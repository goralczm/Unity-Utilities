using UnityEngine;
using Utilities.MapEditor.Tiles;
using Utilities.MapEditor.UI;

namespace Utilities.MapEditor
{
    public class SetupTileButtons : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TileBrush[] _brushes;

        [Header("Instances")]
        [SerializeField] private TileButton _buttonPrefab;
        [SerializeField] private Transform _buttonParent;

        private void Awake()
        {
            for (int i = 0; i < _brushes.Length; i++)
                Instantiate(_buttonPrefab, _buttonParent).Setup(_brushes[i]);
        }
    }
}

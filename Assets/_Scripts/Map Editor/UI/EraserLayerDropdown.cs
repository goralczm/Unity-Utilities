using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

namespace Utilities.MapEditor.UI
{
    public class EraserLayerDropdown : MonoBehaviour
    {
        [SerializeField] private MapEditor _mapEditor;

        private TMP_Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();

            List<string> layers = new List<string>();
            foreach (TilemapLayer layer in Enum.GetValues(typeof(TilemapLayer)))
                layers.Add(layer.ToString());

            _dropdown.AddOptions(layers);
            _dropdown.RefreshShownValue();
        }

        public void SetEraserLayer(int layerIndex)
        {
            _mapEditor.SetEraserTilemap((TilemapLayer)layerIndex);
        }
    }
}

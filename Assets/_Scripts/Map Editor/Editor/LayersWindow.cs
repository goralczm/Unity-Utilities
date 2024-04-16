using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utilities.MapEditor.Editor
{
    public class LayersWindow : EditorWindow
    {
        private MapEditor _levelEditor;
        private Transform _gridParent;
        private TilemapLayer _layer;

        [MenuItem("Window/Level Editor Layers")]
        public static void ShowWindow()
        {
            GetWindow<LayersWindow>().Show();
        }

        private void OnGUI()
        {
            _levelEditor = EditorGUILayout.ObjectField(_levelEditor, typeof(MapEditor), true) as MapEditor;
            _gridParent = EditorGUILayout.ObjectField(_gridParent, typeof(Transform), true) as Transform;
            _layer = (TilemapLayer)EditorGUILayout.EnumPopup(_layer);

            if (GUILayout.Button("Create New Layer"))
            {
                var newLayer = new GameObject(_layer.ToString(), typeof(Tilemap), typeof(TilemapRenderer));
                newLayer.GetComponent<TilemapRenderer>().sortingOrder = _gridParent.GetChild(_gridParent.childCount - 1).GetComponent<TilemapRenderer>().sortingOrder + 1;
                newLayer.transform.SetParent(_gridParent);
                _levelEditor.AddTilemap(newLayer.GetComponent<Tilemap>(), _layer);
            }
        }
    }
}

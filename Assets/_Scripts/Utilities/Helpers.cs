using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Utilities.Utilities
{
    /// <summary>
    /// Set of helper functions accessible from any class.
    /// </summary>
    public static class Helpers
    {
        public static void DestroyChildren(this Transform t)
        {
            for (int i = t.childCount - 1; i >= 0; i--)
                Object.Destroy(t.GetChild(i));
        }

        public static void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static string TwoDecimalPoints(float value)
        {
            if (value % 1 == 0)
                return value.ToString();

            return value.ToString("n2");
        }

        public static bool IsMouseOverUI()
        {
            if (UnityEngine.Input.touchCount > 0)
                return EventSystem.current.IsPointerOverGameObject(0);

            return EventSystem.current.IsPointerOverGameObject();
        }

        public static bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}

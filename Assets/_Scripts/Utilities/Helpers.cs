using UnityEngine;
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
    }
}

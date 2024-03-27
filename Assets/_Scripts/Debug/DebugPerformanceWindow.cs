using UnityEngine;

namespace Utilities.Debug
{
    /// <summary>
    /// Displays the debug window containg performance informations frame rate, memory usage, cpu usage and other.
    /// </summary>
    public class DebugPerformanceWindow : MonoBehaviour
    {
        [SerializeField] private KeyCode _keybinding = KeyCode.F3;

        private bool _isShown;

        private void Update()
        {
            if (Input.GetKeyDown(_keybinding))
                _isShown = !_isShown;
        }

        private void OnGUI()
        {
            if (!_isShown)
                return;

            int items = 5;
            float itemWidth = 180f;
            float itemHeight = 30f;

            float boxHeight = items * itemHeight + 10f;

            GUI.Box(new Rect(0, Screen.height - boxHeight, itemWidth + 20, boxHeight), "");

            GUI.Label(new Rect(10, Screen.height - 5 * itemHeight, 200f, 20), "Performance Debug Window");

            float frameRate = 1.0f / Time.deltaTime;
            GUI.Label(new Rect(10, Screen.height - 4 * itemHeight, 200f, 20), "Frame Rate: " + frameRate.ToString("F2") + " FPS");

            float memoryUsage = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);
            GUI.Label(new Rect(10, Screen.height - 3 * itemHeight, 200f, 20), "Memory Usage: " + memoryUsage.ToString("F2") + " MB");

            float cpuUsage = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / (1024f * 1024f);
            GUI.Label(new Rect(10, Screen.height - 2 * itemHeight, 200f, 20), "CPU Usage: " + cpuUsage.ToString("F2") + " MB");

            string resolution = Screen.width + " x " + Screen.height;
            GUI.Label(new Rect(10, Screen.height - itemHeight, 200f, 20), "Resolution: " + resolution);
        }
    }
}

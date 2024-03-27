using UnityEngine;

namespace Utilities.Debug
{
    /// <summary>
    /// Provides useful device informations for debug purposses.
    /// </summary>
    public static class DeviceInfo
    {
        /// <summary>
        /// Provides useful device informations for debug purposses.
        /// </summary>
        /// <returns>The device information.</returns>
        public static string GetDeviceInfo()
        {
            return SystemInfo.deviceType.ToString() + "\n\n" +
                    SystemInfo.deviceModel.ToString() + "\n\n" +
                    SystemInfo.deviceName.ToString() + "\n\n" +
                    SystemInfo.graphicsDeviceID.ToString() + "\n\n" +
                    SystemInfo.graphicsDeviceType.ToString() + "\n\n" +
                    SystemInfo.graphicsDeviceName.ToString() + "\n\n" +
                    SystemInfo.graphicsDeviceVersion.ToString() + "\n\n" +
                    SystemInfo.operatingSystem.ToString() + "\n\n" +
                    SystemInfo.operatingSystemFamily.ToString() + "\n\n" +
                    SystemInfo.processorType.ToString() + "\n\n" +
                    SystemInfo.processorCount.ToString() + "\n\n" +
                    SystemInfo.processorFrequency.ToString() + "\n\n";
        }
    }
}

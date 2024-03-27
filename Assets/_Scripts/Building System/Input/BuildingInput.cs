using UnityEngine;

namespace Utilities.BuildingSystem.Input
{
    /// <summary>
    /// Object binding the specified <see cref="KeyCode"/> to building.
    /// </summary>
    [System.Serializable]
    public struct BuildingInput
    {
        public GameObject building;
        public KeyCode key;
    }
}

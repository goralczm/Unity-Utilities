using UnityEngine;
using Utilities.PlayerMovement.Input;

namespace Utilities.PlayerMovement.Platformer
{
    /// <summary>
    /// Handles the player visuals with a slight tilt if moved.
    /// </summary>
    public class PlatformerPlayerVisuals : MonoBehaviour
    {
        [Header("Juicyness")]
        [SerializeField] private float _tiltSpeed;
        [SerializeField] private float _maxTilt;

        private void Update()
        {
            Quaternion tiltRotation = Quaternion.Euler(0, 0, _maxTilt * PlayerInput.HorizontalAxis);
            transform.up = Vector3.RotateTowards(transform.up, tiltRotation * Vector2.up, Time.deltaTime * _tiltSpeed, 0f);
        }
    }
}

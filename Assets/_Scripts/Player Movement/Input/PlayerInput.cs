using UnityEngine;

namespace Utilities.PlayerMovement.Input
{
    public static class PlayerInput
    {
        public static float HorizontalAxis => UnityEngine.Input.GetAxis("Horizontal");
        public static float HorizontalAxisRaw => UnityEngine.Input.GetAxisRaw("Horizontal");
        public static float VerticalAxis => UnityEngine.Input.GetAxis("Vertical");
        public static float VerticalAxisRaw => UnityEngine.Input.GetAxisRaw("Vertical");
        public static bool SprintTrigger => UnityEngine.Input.GetKey(KeyCode.LeftShift);
        public static bool JumpTrigger => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public static bool DashTrigger => UnityEngine.Input.GetMouseButtonDown(0);
        public static bool WallGrabTrigger => UnityEngine.Input.GetKey(KeyCode.LeftControl);
    }
}
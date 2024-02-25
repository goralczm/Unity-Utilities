using UnityEngine;

public static class PlayerInput
{
    public static float HorizontalAxis => Input.GetAxis("Horizontal");
    public static float HorizontalAxisRaw => Input.GetAxisRaw("Horizontal");
    public static float VerticalAxis => Input.GetAxis("Vertical");
    public static float VerticalAxisRaw => Input.GetAxisRaw("Vertical");
    public static bool SprintTrigger => Input.GetKey(KeyCode.LeftShift);
    public static bool JumpTrigger => Input.GetKeyDown(KeyCode.Space);
    public static bool DashTrigger => Input.GetMouseButtonDown(0);
    public static bool WallGrabTrigger => Input.GetKey(KeyCode.LeftControl);
}

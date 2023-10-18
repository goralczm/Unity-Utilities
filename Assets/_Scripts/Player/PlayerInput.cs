using UnityEngine;

public static class PlayerInput
{
    public static float HorizontalAxis => Input.GetAxis("Horizontal");
    public static float VerticalAxis => Input.GetAxis("Vertical");
    public static bool SprintTrigger => Input.GetKey(KeyCode.LeftShift);
    public static bool JumpTrigger => Input.GetKeyDown(KeyCode.Space);
}

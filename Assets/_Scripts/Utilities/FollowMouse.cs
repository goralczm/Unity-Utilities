using UnityEngine;
using Utilities.Utilities.Input;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        transform.position = MouseInput.MouseWorldPos;
    }
}

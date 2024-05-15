using UnityEngine;

public class InputManager
{
    public Vector2 KeyMovement => 
        new(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

    public float MouseMotion => Input.GetAxis("Mouse X");
}

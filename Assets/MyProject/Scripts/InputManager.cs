using UnityEngine;

public class InputManager
{
    public Vector2 KeyMovement => 
        new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

    public float MouseMotion => Input.GetAxis("Mouse X");
}

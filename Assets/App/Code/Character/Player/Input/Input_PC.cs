using UnityEngine;

public class Input_PC : IPlayerInput
{
    public float GetHorizontalInput()=> Input.GetAxis("Horizontal");
    public float GetVerticalInput()=> Input.GetAxis("Vertical");
    public bool GetJumpInput() => Input.GetButtonDown("Jump");
    public bool GetSprintInput() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    public bool GetWalkInput()=> Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
}

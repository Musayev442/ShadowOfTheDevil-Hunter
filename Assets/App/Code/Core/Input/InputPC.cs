using System.Collections;
using UnityEngine;
using Assets.App.Code.Core.Input;

public class InputPC : IInputService
{
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float VerticalInput => Input.GetAxis("Vertical");
    public Vector2 InputDirection => new Vector3(HorizontalInput, VerticalInput);
    public bool JumpPressed => Input.GetButtonDown("Jump");
    public bool IsSprinting => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    public bool IsWalking => Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    public bool IsCrouching => Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
    public bool HasMovementInput => InputDirection.magnitude > 0.1f;
}
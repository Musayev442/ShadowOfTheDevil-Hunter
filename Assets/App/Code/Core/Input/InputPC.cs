using System.Collections;
using UnityEngine;
using Assets.App.Code.Core.Input;

public class InputPC : IInputService
{
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float VerticalInput => Input.GetAxis("Vertical");
    public Vector2 MovementInput => new Vector3(HorizontalInput, VerticalInput);
    public bool JumpPressed => Input.GetButtonDown("Jump");
    public bool IsSprinting => Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    public bool IsWalking => Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
    public bool IsCrouching => Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt);
    public bool HasMovementInput => MovementInput.magnitude > 0.1f;
}
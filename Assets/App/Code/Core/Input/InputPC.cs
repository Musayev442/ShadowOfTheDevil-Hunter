using UnityEngine;

namespace App.Code.Core.Input
{
    public class InputPC : IInputService
    {
        public float MouseX => UnityEngine.Input.GetAxis("Mouse X");
        public float MouseY => UnityEngine.Input.GetAxis("Mouse Y");
        public float ScrollInput => UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        public float HorizontalInput => UnityEngine.Input.GetAxis("Horizontal");
        public float VerticalInput => UnityEngine.Input.GetAxis("Vertical");
        public bool RotateCameraLeft => UnityEngine.Input.GetKey(KeyCode.Q);
        public bool RotateCameraRight => UnityEngine.Input.GetKey(KeyCode.E);
        public Vector2 InputDirection => new Vector3(HorizontalInput, VerticalInput);
        public bool JumpPressed => UnityEngine.Input.GetButtonDown("Jump");
        public bool IsSprinting => UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift);
        public bool IsWalking => UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl);
        public bool IsCrouching => UnityEngine.Input.GetKey(KeyCode.LeftAlt) || UnityEngine.Input.GetKey(KeyCode.RightAlt);
        public bool HasMovementInput => InputDirection.magnitude > 0.1f;
        public bool IsLockedOn => UnityEngine.Input.GetKeyDown(KeyCode.Tab);
        public bool SwitchTargetLeft => UnityEngine.Input.GetKeyDown(KeyCode.R);
        public bool SwitchTargetRight  => UnityEngine.Input.GetKeyDown(KeyCode.T);
    }
}
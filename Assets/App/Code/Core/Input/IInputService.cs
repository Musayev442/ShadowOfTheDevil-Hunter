using UnityEngine;

namespace App.Code.Core.Input
{
    public interface IInputService
    {
        float MouseX { get; }
        float MouseY { get; }
        float ScrollInput { get; }
        float HorizontalInput { get; }
        float VerticalInput { get; }
        bool RotateCameraLeft { get; }
        bool RotateCameraRight { get; }
        bool JumpPressed { get; }
        bool IsSprinting { get; }
        bool IsWalking { get; }
        bool IsCrouching { get; }
        bool HasMovementInput { get; }
        bool IsLockedOn { get; }
        bool SwitchTargetLeft { get; }
        bool SwitchTargetRight { get; }
        Vector2 InputDirection { get; }
        
        
    }
}
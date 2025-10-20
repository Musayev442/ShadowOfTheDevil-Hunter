using UnityEngine;

namespace App.Code.Core.Input
{
    public interface IInputService
    {
        float HorizontalInput { get; }
        float VerticalInput { get; }
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
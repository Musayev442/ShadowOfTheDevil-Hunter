using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Core.Input
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
        Vector2 InputDirection { get; }
    }
}
using UnityEngine;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IAnimationSystem
    {
        void SetMovementSpeed(float speed);
        void SetGrounded(bool isGrounded);
        void SetVerticalVelocity(float velocity);
        void SetTrigger();
    }
}
using UnityEngine;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IAnimationSystem
    {
        void UpdateMovementAnimation(Vector2 movementInput, bool isRunning = false);
    }
}
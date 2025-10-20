using UnityEngine;

namespace App.Code.Core.Systems.LockOnSystem.Interfaces
{
    public interface ILockOnSystem
    {
        void ToggleLockOn();                          // Enable/disable lock-on
        void SwitchTarget(Vector2 direction);         // Switch to next target (left/right)
        void UpdateLockOn();                          // Update lock-on state each frame
        bool IsLockedOn();                            // Check if currently locked on
        Transform GetCurrentTarget();                 // Get the locked target
    }
}
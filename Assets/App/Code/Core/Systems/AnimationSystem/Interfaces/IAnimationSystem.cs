using App.Code.Core.Systems.CombatSystem.Interfaces;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.Interfaces
{
    public interface IAnimationSystem
    {
        void UpdateMovementAnimation(float targetSpeed);
        void UpdateJumpAnimation(IJumpable jumpSystem);
        void UpdateLockOnAnimation(ILockOnSystem lockOnSystem, Vector2 moveInput);  // ✅ NEW
        void UpdateAttackAnimation(CombatSystem.Interfaces.ICombatSystem attackSystem);
        bool IsInLandingAnimation();
        
    }
}
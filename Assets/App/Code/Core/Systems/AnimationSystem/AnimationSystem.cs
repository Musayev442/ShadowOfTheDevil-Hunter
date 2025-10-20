using App.Code.Core.Systems.CombatSystem.Interfaces;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.LockOnSystem.Interfaces;
using UnityEngine;
using ICombatSystem = App.Code.Core.Systems.CombatSystem.Interfaces.ICombatSystem;

namespace App.Code.Core.Systems.AnimationSystem
{
    public class AnimationSystem : IAnimationSystem
    {
        private Animator _animator;
        private AnimationConfig _config;

        // Normal movement state
        private float _currentBlendValue;

        // Lock-on movement state (NEW)
        private float _currentHorizontal;
        private float _currentVertical;

        // Landing state
        private bool _isLanding;
        private float _landingTimer;
        private float _landingDuration = 0.3f;

        // Attack tracking
        private int _previousAttackIndex = -1;

        public AnimationSystem(Animator animator, AnimationConfig config)
        {
            _animator = animator;
            _config = config;

            // Initialize values
            _currentBlendValue = 0f;
            _currentHorizontal = 0f; // NEW
            _currentVertical = 0f; // NEW
            _isLanding = false;
            _landingTimer = 0f;
            _previousAttackIndex = -1;
        }

        public void UpdateMovementAnimation(float targetSpeed)
        {
            if (_animator == null) return;

            // ===== NORMAL MODE (ORIGINAL) =====
            // Use MoveSpeed for 1D blend tree

            _currentBlendValue = Mathf.Lerp(_currentBlendValue, targetSpeed, Time.deltaTime * _config.blendSpeed);
            _animator.SetFloat(_config.moveSpeedParam, _currentBlendValue);

            // Smoothly reset lock-on parameters to 0 (NEW)
            _currentHorizontal = Mathf.Lerp(_currentHorizontal, 0f, Time.deltaTime * _config.lockOnBlendSpeed);
            _currentVertical = Mathf.Lerp(_currentVertical, 0f, Time.deltaTime * _config.lockOnBlendSpeed);
            _animator.SetFloat(_config.horizontalParam, _currentHorizontal);
            _animator.SetFloat(_config.verticalParam, _currentVertical);
        }

        public void UpdateJumpAnimation(IJumpable jumpSystem)
        {
            if (_animator == null) return;

            bool isGrounded = jumpSystem.IsGrounded();

            // Update grounded state (for Fall → Land transition)
            _animator.SetBool(_config.isGroundedParam, isGrounded);

            // Trigger jump (to enter sub-state machine)
            if (jumpSystem.HasJustJumped())
            {
                _animator.SetTrigger(_config.jumpTriggerParam);
            }
        }

        public void UpdateLockOnAnimation(ILockOnSystem lockOnSystem, Vector2 moveInput)
        {
            if (_animator == null) return;

            bool isLockedOn = lockOnSystem.IsLockedOn();

            // Set lock-on state in animator
            _animator.SetBool(_config.isLockedOnParam, isLockedOn);

            if (isLockedOn)
            {
                // Update strafe blend tree parameters (2D blend tree uses raw input)
                _animator.SetFloat(_config.horizontalParam, moveInput.x);
                _animator.SetFloat(_config.verticalParam, moveInput.y);
            }
        }

        public void UpdateAttackAnimation(ICombatSystem attackSystem)
        {
            throw new System.NotImplementedException();
        }

        public bool IsInLandingAnimation()
        {
            return false;
        }
    }
}
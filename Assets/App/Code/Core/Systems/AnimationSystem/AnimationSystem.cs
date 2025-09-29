using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.AnimationSystem
{
    public class AnimationSystem : IAnimationSystem
    {
        private readonly int _horizontal = Animator.StringToHash("Horizontal");
        private readonly int _vertical = Animator.StringToHash("Vertical");
        private readonly Animator _animator;
        private readonly float _smoothTime;
    
        // Current smoothed values
        private float _currentHorizontal;
        private float _currentVertical;
        private float _verticalVelocity;

        public AnimationSystem(Animator animator, float smoothTime = 0.1f)
        {
            this._animator = animator;
            this._smoothTime = smoothTime;
        }
    
        public void UpdateMovementAnimation(Vector2 movementInput, bool isRunning = false)
        {
            float targetVertical = movementInput.magnitude; // magnitude = speed (0 → idle, 1 → walk)

            // Apply running speed multiplier
            if (isRunning && targetVertical > 0.1f)
            {
                targetVertical *= 2f; // double for running
            }

            // SmoothDamp towards target value
            _currentVertical = Mathf.SmoothDamp(
                _currentVertical,
                targetVertical,
                ref _verticalVelocity,
                _smoothTime,
                Mathf.Infinity, 
                Time.deltaTime
            );

            // Set only the Vertical parameter
            _animator.SetFloat(_vertical, _currentVertical);
        }
    }
}
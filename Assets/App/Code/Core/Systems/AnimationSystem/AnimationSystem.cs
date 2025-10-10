using App.Code.Core.Systems.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.AnimationSystem
{
    public class AnimationSystem : IAnimationSystem
    {
        private static readonly int Jump = Animator.StringToHash("Jump");
        private readonly Animator _animator;
        private readonly int _speedHash;
        private readonly int _isGroundedHash;
        private readonly int _verticalVelocityHash;
        private readonly float _dampTime;

        public AnimationSystem(Animator animator, float dampTime = 0.3f)
        {
            _animator = animator;
            _dampTime = dampTime;
            _speedHash = Animator.StringToHash("Speed");
            _isGroundedHash = Animator.StringToHash("IsGrounded");
            _verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
        }

        public void SetMovementSpeed(float speed)
        {
            if (_animator == null) return;
        
            // Set animator parameter - single float for idle/walk/run blend
            _animator.SetFloat(_speedHash, speed, _dampTime, Time.deltaTime);
        }
        
        public void SetGrounded(bool isGrounded)
        {
            _animator.SetBool(_isGroundedHash, isGrounded);
        }

        public void SetVerticalVelocity(float velocity)
        {
            _animator.SetFloat(_verticalVelocityHash, velocity);
        }

        public void SetTrigger()
        {
            _animator.SetTrigger(Jump);
        }
    }
}
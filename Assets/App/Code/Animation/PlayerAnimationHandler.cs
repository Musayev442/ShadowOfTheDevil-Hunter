using Assets.App.Code.Animation.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation
{
    public class PlayerAnimationHandler : BaseAnimationHandler, ISprintAnimation, IJumpAnimation
    {
        private static readonly int SPRINT_PARAM = Animator.StringToHash("Sprint");
        private static readonly int JUMP_START_TRIGGER = Animator.StringToHash("JumpStart");
        private static readonly int JUMP_FALLING_TRIGGER = Animator.StringToHash("JumpFalling");
        private static readonly int JUMP_END_TRIGGER = Animator.StringToHash("JumpEnd");

        public bool IsSprinting { get; private set; }
        public bool IsJumping { get; private set; }

        public PlayerAnimationHandler(Animator animator) : base(animator) { }

        public void PlaySprint()
        {
            if (!IsSprinting)
            {
                animator.SetBool(SPRINT_PARAM, true);
                IsSprinting = true;
            }
        }

        public void StopSprint()
        {
            if (IsSprinting)
            {
                animator.SetBool(SPRINT_PARAM, false);
                IsSprinting = false;
            }
        }

        public void PlayJumpStart()
        {
            animator.SetTrigger(JUMP_START_TRIGGER);
            IsJumping = true;
        }

        public void PlayJumpFalling()
        {
            animator.SetTrigger(JUMP_FALLING_TRIGGER);
        }

        public void PlayJumpEnd()
        {
            animator.SetTrigger(JUMP_END_TRIGGER);
            IsJumping = false;
        }

        public override void ResetAnimationStates()
        {
            IsSprinting = false;
            IsJumping = false;
            animator.SetBool(SPRINT_PARAM, false);
        }
    }

}
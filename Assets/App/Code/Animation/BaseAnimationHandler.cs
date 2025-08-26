using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation
{
    public abstract class BaseAnimationHandler : IAnimationHandler
    {
        protected readonly Animator animator;

        protected static readonly int SPEED_PARAM = Animator.StringToHash("Speed");
        protected static readonly int HORIZONTAL_PARAM = Animator.StringToHash("Horizontal");
        protected static readonly int VERTICAL_PARAM = Animator.StringToHash("Vertical");
        protected static readonly int ANIMATION_SPEED_PARAM = Animator.StringToHash("AnimationSpeed");

        protected float blendDampTime = 0.1f;

        protected BaseAnimationHandler(Animator animator)
        {
            this.animator = animator;
        }

        public virtual void SetMovementBlendTree(float speed, float horizontal, float vertical)
        {
            animator.SetFloat(SPEED_PARAM, speed, blendDampTime, Time.deltaTime);
            animator.SetFloat(HORIZONTAL_PARAM, horizontal, blendDampTime, Time.deltaTime);
            animator.SetFloat(VERTICAL_PARAM, vertical, blendDampTime, Time.deltaTime);
        }

        public void SetAnimationSpeed(float speed)
        {
            animator.SetFloat(ANIMATION_SPEED_PARAM, speed);
        }

        public abstract void ResetAnimationStates();
    }

}
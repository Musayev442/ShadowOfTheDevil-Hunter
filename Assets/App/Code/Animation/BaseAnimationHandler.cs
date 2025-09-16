using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Animation
{
    public abstract class BaseAnimationHandler : IAnimationHandler
    {
        protected Animator animator;
        protected string currentState;

        // Animation parameters
        protected const string MOVEMENT_SPEED = "MoveSpeed";
        protected const string IS_GROUNDED = "IsGrounded";
        protected const string ATTACK_TRIGGER = "Attack";
        protected const string DODGE_TRIGGER = "Dodge";
        protected const string STAGGER_TRIGGER = "Stagger";
        protected const string DEATH_TRIGGER = "Die";
        protected const string IS_DEAD = "IsDead";

        public virtual void Initialize(Animator animator)
        {
            this.animator = animator;
        }

        public virtual void UpdateAnimationState()
        {
            // Base implementation can be overridden
        }

        public virtual void SetFloat(string parameter, float value)
        {
            animator.SetFloat(parameter, value);
        }

        public virtual void SetBool(string parameter, bool value)
        {
            animator.SetBool(parameter, value);
        }

        public virtual void SetTrigger(string parameter)
        {
            animator.SetTrigger(parameter);
        }

        public virtual void ResetTrigger(string parameter)
        {
            animator.ResetTrigger(parameter);
        }

        protected virtual void ChangeAnimationState(string newState)
        {
            if (currentState == newState) return;

            animator.Play(newState);
            currentState = newState;
        }
    }

}
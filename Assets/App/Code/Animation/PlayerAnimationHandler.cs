using Assets.App.Code.Animation.Interfaces;
using SotD.Characters.Player;
using Assets.App.Code.StateMachine;
using UnityEngine;
using Assets.App.Code.Character.Player.States;

namespace Assets.App.Code.Animation
{
    public class PlayerAnimationHandler : BaseAnimationHandler
    {
        private PlayerController player;
        private SoulStateMachine soulStateMachine;

        // Player-specific parameters
        private const string IS_LOCKED_ON = "IsLockedOn";
        private const string VERTICAL_INPUT = "Vertical";
        private const string HORIZONTAL_INPUT = "Horizontal";
        private const string IS_SPRINTING = "IsSprinting";
        private const string ATTACK_TYPE = "AttackType";

        public void Initialize(PlayerController player, Animator animator)
        {
            base.Initialize(animator);
            this.player = player;
            this.soulStateMachine = player.stateMachine;
        }

        public override void UpdateAnimationState()
        {
            UpdateMovementAnimation();
            UpdateCombatAnimation();
            UpdateStateBasedAnimation();
        }

        private void UpdateMovementAnimation()
        {
            // Set movement speed based on input magnitude
            float moveSpeed = player.movementMagnitude;
            SetFloat(MOVEMENT_SPEED, moveSpeed);

            // Set directional inputs for blend trees
            SetFloat(HORIZONTAL_INPUT, player.movementInput.x);
            SetFloat(VERTICAL_INPUT, player.movementInput.y);

            // Lock-on state
            SetBool(IS_LOCKED_ON, player.IsLockedOn);
        }

        private void UpdateCombatAnimation()
        {
            // Update combat-related animations
            SetBool(IS_GROUNDED, true); // Always grounded for simplicity
        }

        private void UpdateStateBasedAnimation()
        {
            // Handle state-specific animations
            switch (soulStateMachine.currentState)
            {
                case PlayerAttackState attackState:
                    HandleAttackAnimation();
                    break;
                case PlayerDodgeState dodgeState:
                    HandleDodgeAnimation();
                    break;
                case PlayerStaggerState staggerState:
                    HandleStaggerAnimation();
                    break;
                case PlayerDeadState deadState:
                    HandleDeathAnimation();
                    break;
            }
        }

        private void HandleAttackAnimation()
        {
            // Different attack animations based on input or weapon type
            SetTrigger(ATTACK_TRIGGER);
        }

        private void HandleDodgeAnimation()
        {
            SetTrigger(DODGE_TRIGGER);
        }

        private void HandleStaggerAnimation()
        {
            SetTrigger(STAGGER_TRIGGER);
        }

        private void HandleDeathAnimation()
        {
            SetTrigger(DEATH_TRIGGER);
            SetBool(IS_DEAD, true);
        }

        // Public methods for external control
        public void PlayAttackAnimation(int attackType = 0)
        {
            SetFloat(ATTACK_TYPE, attackType);
            SetTrigger(ATTACK_TRIGGER);
        }

        public void PlayDodgeAnimation()
        {
            SetTrigger(DODGE_TRIGGER);
        }

        public void PlayHitReaction()
        {
            SetTrigger(STAGGER_TRIGGER);
        }
    }

}
using Assets.App.Code.StateMachine;
using SotD.Characters.Player;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Character.Player.States
{
    public class PlayerJumpingState : IState
    {
        private PlayerController player;
        private float jumpTimer;

        public PlayerJumpingState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            Debug.Log("Player entered Jumping state");
            player.animator.SetTrigger("Jump");

            // Apply jump force
            if (player.rb != null)
            {
                player.rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
            }

            jumpTimer = 0f;
        }

        public void Execute()
        {
            // LOGIC ONLY - State transitions and timer updates
            jumpTimer += Time.deltaTime;

            // Check if landed (simple ground check)
            if (player.IsGrounded() && jumpTimer > 0.1f) // Small delay to prevent immediate transition
            {
                // Determine which state to transition to based on cached input
                if (player.movementMagnitude > 0.1f)
                {
                    if (player.isWalking)
                    {
                        player.stateMachine.ChangeState(player.walkingState);
                    }
                    else if (player.isSprinting)
                    {
                        player.stateMachine.ChangeState(player.sprintingState);
                    }
                    else
                    {
                        // Default to running
                        player.stateMachine.ChangeState(player.runningState);
                    }
                }
                else
                {
                    player.stateMachine.ChangeState(player.idleState);
                }
            }
        }

        public void ExecutePhysics()
        {
            // PHYSICS ONLY - Limited air control
            Vector3 airDirection = player.movementDirection;
            if (airDirection.magnitude > 0.1f)
            {
                // Reduced air control speed
                player.movement.Move(airDirection, player.runSpeed * 0.3f);
                player.movement.RotateTowards(airDirection);
            }
        }
        public void Exit()
        {
            Debug.Log("Player exited Jumping state");
        }
    }
}
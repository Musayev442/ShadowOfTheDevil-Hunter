using Assets.App.Code.StateMachine;
using SotD.Characters;
using SotD.Characters.Player;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerWalkingState : IState
{
    private PlayerController player;

    public PlayerWalkingState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Player entered Walking state");
        // Animator parameters are handled in PlayerController.UpdateAnimatorParameters()
    }

    public void Execute()
    {
        // LOGIC ONLY - State transitions and checks
        if (player.movementMagnitude < 0.1f)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
        else if (!player.isWalking) // If not holding Ctrl anymore
        {
            if (player.isSprinting)
            {
                player.stateMachine.ChangeState(player.sprintingState);
            }
            else
            {
                // Default to running
                player.stateMachine.ChangeState(player.runningState);
            }
        }

        if (player.jumpPressed)
        {
            player.stateMachine.ChangeState(player.jumpingState);
        }
    }

    public void ExecutePhysics()
    {
        // PHYSICS ONLY - Handle movement
        Vector3 movementDirection = player.movementDirection;

        if (movementDirection.magnitude > 0.1f)
        {
            player.movement.Move(movementDirection, player.walkSpeed);
            player.movement.RotateTowards(movementDirection);
        }
        else
        {
            // Stop movement smoothly
            player.movement.Move(Vector3.zero, 0f);
        }
    }

    public void Exit()
    {
        Debug.Log("Player exited Walking state");
    }
}


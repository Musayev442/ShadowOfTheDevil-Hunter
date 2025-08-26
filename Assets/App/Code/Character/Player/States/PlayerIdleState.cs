using Assets.App.Code.StateMachine;
using SotD.Characters;
using SotD.Characters.Player;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController player;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Player entered Idle state");
        // Animator parameters are handled in PlayerController.UpdateAnimatorParameters()
    }
    public void Execute()
    {
        // Check for transitions using cached input
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
                // Default movement is running
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
        // PHYSICS ONLY - No movement in idle state
        // Could add idle physics like slight swaying, breathing animation, etc.
    }

    public void Exit()
    {
        Debug.Log("Player exited Idle state");
    }
}


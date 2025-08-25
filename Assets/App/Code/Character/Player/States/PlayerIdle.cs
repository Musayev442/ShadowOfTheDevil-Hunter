using Assets.App.Code.StateMachine;
using SotD.Characters;
using SotD.Characters.Player;
using UnityEngine;

public class PlayerIdle : IState
{
    private PlayerController player;

    public PlayerIdle(PlayerController player)
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
            else
            {
                player.stateMachine.ChangeState(player.runningState);
            }
        }

        if (player.isJumpPressed)
        {
            player.stateMachine.ChangeState(player.jumpingState);
        }
    }

    public void Exit()
    {
        Debug.Log("Player exited Idle state");
    }
}


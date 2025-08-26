using Assets.App.Code.StateMachine;
using SotD.Characters.Player;
using UnityEngine;

public class PlayerRunningState : IState
{
    private PlayerController player;

    public PlayerRunningState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Player entered Running state");
        // Animator parameters are handled in PlayerController.UpdateAnimatorParameters()
    }

    public void Execute()
    {
        // LOGIC ONLY - State transitions and checks
        if (player.movementMagnitude < 0.1f)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
        else if (player.isWalking) // If Ctrl is pressed
        {
            player.stateMachine.ChangeState(player.walkingState);
        }
        else if (player.isSprinting) // If Shift is pressed
        {
            player.stateMachine.ChangeState(player.sprintingState);
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
            player.movement.Move(movementDirection, player.runSpeed);
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
        Debug.Log("Player exited Running state");
    }
}

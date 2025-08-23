using Assets.App.Code.StateMachine;
using SotD.Characters;
using SotD.Characters.Player;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerWalk : IState
{
    private readonly PlayerController player;

    public PlayerWalk(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Optional: reset triggers or effects
    }

    public void Execute()
    {
      
    }

    public void Exit()
    {
        // Optional: cleanup
    }
}


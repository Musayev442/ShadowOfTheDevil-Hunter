using Assets.App.Code.StateMachine;
using SotD.Characters;
using SotD.Characters.Player;
using UnityEngine;

public class PlayerIdle : IState
{
    private readonly Character character;

    public PlayerIdle(PlayerController player)
    {
        this.character = player;
    }

    public void Enter() { }

    public void Execute()
    {
        
    }

    public void Exit() { }
}


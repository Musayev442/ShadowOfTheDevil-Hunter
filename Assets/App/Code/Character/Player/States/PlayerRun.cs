using UnityEngine;

public interface PlayerRun
{
    void Enter();
    void Exit();
    void Update();
    bool CanTransition(PlayerIdle newState);
}

using UnityEngine;

public interface PlayerIdle
{
    void Enter();
    void Exit();
    void Update();
    bool CanTransition(PlayerIdle newState);
}

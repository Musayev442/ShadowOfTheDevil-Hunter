using UnityEngine;

public interface PlayerMove
{
    void Enter();
    void Exit();
    void Update();
    bool CanTransition(PlayerIdle newState);
}

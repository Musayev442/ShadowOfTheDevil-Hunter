using System.Collections;
using UnityEngine;

namespace Assets.App.Code.StateMachine
{
    public interface IState
    {
        void Enter();
        void Execute();        // Logic updates (transitions, animations, etc.)
        void ExecutePhysics(); // Physics updates (movement, forces, etc.)
        void Exit();
    }
}
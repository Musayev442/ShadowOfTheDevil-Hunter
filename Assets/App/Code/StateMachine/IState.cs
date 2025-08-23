using System.Collections;
using UnityEngine;

namespace Assets.App.Code.StateMachine
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
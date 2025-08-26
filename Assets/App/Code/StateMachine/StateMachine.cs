using System.Collections;
using UnityEngine;

namespace Assets.App.Code.StateMachine
{
    public class StateMachine
    {
        private IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void UpdateState()
        {
            currentState?.Execute();
        }

        public void UpdatePhysics()
        {
            currentState?.ExecutePhysics();
        }
    }
}
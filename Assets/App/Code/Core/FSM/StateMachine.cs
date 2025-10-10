
using Assets.App.Code.Core.FSM;

namespace App.Code.Core.FSM
{
    public class StateMachine
    {

        public IState CurrentState { get; private set; }
        public StateTransitionSystem stateTransitionSystem;

        public StateMachine()
        {
            stateTransitionSystem = new StateTransitionSystem();
        }
        
        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void UpdateState()
        {
            CurrentState?.ExecuteUpdate();
            
            // Check for transitions first, some transitions depend on physics calculations (ground detection, collisions, etc.)
            IState nextState = stateTransitionSystem.CheckTransitions(CurrentState);
            if (nextState != null)
            {
                ChangeState(nextState);
                return;
            }
        }

        public void UpdatePhysics()
        {
            CurrentState?.ExecutePhysics();
        }
    }
}
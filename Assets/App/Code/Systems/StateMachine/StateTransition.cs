using UnityEngine;

public class StateTransition : MonoBehaviour
{
    public IState FromState { get; private set; }
    public IState ToState { get; private set; }
    public StateTransition(IState fromState, IState toState)
    {
        FromState = fromState;
        ToState = toState;
    }
    public bool IsValidTransition()
    {
        return FromState != null && ToState != null && FromState.CanTransition(ToState);
    }
    public void ExecuteTransition(StateMachine stateMachine)
    {
        if (IsValidTransition())
        {
            stateMachine.SetState(ToState);
        }
        else
        {
            Debug.LogWarning("Invalid state transition attempted.");
        }
    }
}

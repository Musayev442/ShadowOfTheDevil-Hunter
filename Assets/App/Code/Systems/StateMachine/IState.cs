using UnityEngine;

public class IState
{
    // This interface defines the methods that any state must implement
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    // Determines if transition to the given state is allowed
    public virtual bool CanTransition(IState newState) { return true; }
}

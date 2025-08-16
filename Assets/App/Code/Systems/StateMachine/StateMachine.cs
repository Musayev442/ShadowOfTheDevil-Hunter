using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;

    public void SetState(IState newState)
    {
        if (currentState != null && !currentState.CanTransition(newState))
        {
            Debug.LogWarning("Cannot transition to the new state.");
            return;
        }
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}

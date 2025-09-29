using System;
using System.Collections.Generic;
using Assets.App.Code.Core.FSM;

namespace App.Code.Core.FSM
{
    public class StateTransitionSystem
    {
        private List<StateTransition> _transitions = new List<StateTransition>();

        public void AddStateTransition(IState from, Func<bool> condition, IState to, int priority)
        {
            _transitions.Add(new StateTransition(from, condition, to, priority));
        }

        public IState CheckTransitions(IState currentState)
        {
            List<StateTransition> applicableTransitions = new List<StateTransition>();

            foreach (var t in _transitions)
            {
                if (t.From == currentState && t.Condition())
                {
                    applicableTransitions.Add(t);
                }
            }

            // Sort by priority descending
            applicableTransitions.Sort((a, b) => b.Priority.CompareTo(a.Priority));

            return applicableTransitions.Count > 0 ? applicableTransitions[0].To : null;
        }
    }
}
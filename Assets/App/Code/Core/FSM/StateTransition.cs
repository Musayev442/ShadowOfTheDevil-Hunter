using System;

namespace App.Code.Core.FSM
{
    public struct StateTransition
    {
        private IState _from;
        private IState _to;
        private Func<bool> _condition;
        private int _priority;

        public IState From => _from;
        public IState To => _to;
        public Func<bool> Condition => _condition;
        public int Priority => _priority;

        public StateTransition(IState from, Func<bool> condition, IState to, int priority)
        {
            _from = from;
            _to = to;
            _condition = condition;
            _priority = priority;
        }
    }
}
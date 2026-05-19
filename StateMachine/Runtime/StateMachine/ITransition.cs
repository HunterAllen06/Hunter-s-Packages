using System;

namespace HunterAllen.StateMachine
{
    public interface ITransition
    {
        public IState To { get; }
        public ICondition Condition { get; }
        public Action OnTransition { get; }
    }
}
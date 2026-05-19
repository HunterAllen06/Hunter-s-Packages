using System;

namespace HunterAllen.StateMachine
{
    /// <summary>
    /// Contains a state to transition to, a condition to be met in order to transition, and an action to be invoked upon transition.
    /// </summary>
    public class Transition : ITransition
    {
        public IState To { get; }
        public ICondition Condition { get; }
        public Action OnTransition { get; }

        public Transition(IState to, ICondition condition, Action onTransition = null) { To = to; Condition = condition; OnTransition = onTransition; }
    }
}
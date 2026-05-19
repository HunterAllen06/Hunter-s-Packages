using System;
using System.Collections.Generic;

namespace HunterAllen.StateMachine
{
    /// <summary>
    /// An abstract container for any kind of behaviour, and transitions to next states.
    /// <para>The generic field is the state machine this state should refer to as 'context'
    /// (T is passed into this state's constructor function)</para>
    /// </summary>
    /// <typeparam name="T">The state machine this state should refer to as 'context' (T is passed into this state's constructor function)</typeparam>
    public abstract class BaseState<T> : IState
    {
        public bool IsStateActive { get; private set; }
        
        public T Context;
        public ICondition DefaultExitCondition;
        public List<ITransition> Transitions = new();
        public event Action OnEnter;
        public event Action OnExit;

        public BaseState(T context)
        {
            Context = context;
        }
        public BaseState(T context, ICondition defaultExitCondition)
        {
            Context = context;
            DefaultExitCondition = defaultExitCondition;
        }
        public BaseState(T context, Func<bool> defaultExitCondition)
        {
            Context = context;
            DefaultExitCondition = new Condition(defaultExitCondition);
        }

        /// <summary>
        /// Adds a state that can be transitioned to given the input condition returns true.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition on which to transition to the given state.</param>
        /// <param name="onTransition">An optional event delegate to invoke upon transition.</param>
        public void AddTransition(IState to, ICondition condition, Action onTransition = null)
        {
            Transitions.Add(new Transition(to, condition, onTransition));
        }
        /// <summary>
        /// Adds a state that can be transitioned to given the input condition returns true.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition on which to transition to the given state.</param>
        /// <param name="onTransition">An optional event delegate to invoke upon transition.</param>
        public void AddTransition(IState to, Func<bool> condition, Action onTransition = null)
        {
            Transitions.Add(new Transition(to, new Condition(condition), onTransition));
        }
        public void RemoveTransition(IState to)
        {
            foreach (var transition in Transitions)
            {
                if (transition.To == to)
                {
                    Transitions.Remove(transition);
                }
            }
        }
        public void ClearAllTransitions()
        {
            Transitions.Clear();
        }

        /// <summary>
        /// Called when this state is entered.
        /// </summary>
        public virtual void OnEnterState() { }
        /// <summary>
        /// Called when this state is exited.
        /// </summary>
        public virtual void OnUpdateState() { }
        /// <summary>
        /// Called in FixedUpdate if this state is active.
        /// </summary>
        public virtual void OnFixedUpdateState() { }
        /// <summary>
        /// Called in Update if this state is active.
        /// </summary>
        public virtual void OnExitState() { }

        public void EnterState()
        {
            IsStateActive = true;
            OnEnter?.Invoke();
            OnEnterState();
        }
        public void ExitState()
        {
            IsStateActive = false;
            OnExit?.Invoke();
            OnExitState();
        }
    }
}
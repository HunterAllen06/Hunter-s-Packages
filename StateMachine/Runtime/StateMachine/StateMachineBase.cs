using System;
using System.Collections.Generic;

namespace HunterAllen.StateMachine
{
    /// <summary>
    /// Controls and manages the flow and transitions of two or more states.
    /// </summary>
    /// <typeparam name="T">The type of the class that inherits from StateMachine.</typeparam>
    public class StateMachineBase<T>
    {
        /// <summary>
        /// The current active state.
        /// </summary>
        public BaseState<T> CurrentState;
        /// <summary>
        /// A list of all states that this state machine manages.
        /// </summary>
        public List<BaseState<T>> ParallelStates = new();
        /// <summary>
        /// A list of transitions that can happen at any time regardless of the current state.
        /// </summary>
        public List<Transition> GlobalTransitions = new();

        public StateMachineBase() { }

        ITransition _transition;
        /// <summary>
        /// Check to see if a transition to a next state is available and update the current state.
        /// </summary>
        public virtual void Update()
        {
            if (CurrentState != null)
            {
                _transition = GetTransition();

                if (_transition != null)
                {
                    TransitionToState(_transition);
                }

                CurrentState?.OnUpdateState();
            }

            foreach (var state in ParallelStates)
            {
                if (state.IsStateActive)
                {
                    state.OnUpdateState();
                }
            }
        }
        /// <summary>
        /// Call fixed update on the current state and active parallel states.
        /// </summary>
        public virtual void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.OnFixedUpdateState();
            }

            foreach (var state in ParallelStates)
            {
                if (state.IsStateActive)
                {
                    state.OnFixedUpdateState();
                }
            }
        }

        /// <summary>
        /// Adds a transition that can occur during any state.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition on which to transition to the given state.</param>
        /// <param name="onTransition">An optional event delegate to invoke upon transition.</param>
        public virtual void AddGlobalTransition(IState to, ICondition condition, Action onTransition = null)
        {
            GlobalTransitions.Add(new Transition(to, condition, onTransition));
        }

        /// <summary>
        /// Checks all available transitions and returns one if its condition is met.
        /// </summary>
        /// <returns>The first transition that has a condition that returns true.</returns>
        public virtual ITransition GetTransition()
        {
            foreach (var transition in GlobalTransitions)
            {
                if (transition.Condition.EvaluateCondition())
                {
                    return transition;
                }
            }
            foreach (var transition in CurrentState.Transitions)
            {
                if (transition.Condition.EvaluateCondition())
                {
                    return transition;
                }
            }
            foreach (var state in ParallelStates)
            {
                if (!state.IsStateActive)
                {
                    continue;
                }

                foreach (var transition in state.Transitions)
                {
                    if (transition.Condition.EvaluateCondition())
                    {
                        return transition;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Exits the current state if one is active, and enters the next state.
        /// <para>If you want to set the current state without a transition, use SetCurrentState(IState state) instead.</para>
        /// </summary>
        /// <param name="transition">The transition to use.</param>
        public void TransitionToState(ITransition transition)
        {
            // Only return if current state isn't null and we 
            // are trying to transition to the current state
            if (CurrentState != null && transition.To == CurrentState)
            {
                return;
            }

            CurrentState?.ExitState();
            transition.OnTransition?.Invoke();
            CurrentState = transition.To as BaseState<T>;
            CurrentState.EnterState();
        }
        /// <summary>
        /// Exits the current state if one is active, and enters the next state.
        /// </summary>
        /// <param name="state">The state to enter.</param>
        public void SetCurrentState(IState state)
        {
            // Only return if current state isn't null and we 
            // are trying to transition to the current state
            if (CurrentState != null && state == CurrentState)
            {
                return;
            }

            CurrentState?.ExitState();
            CurrentState = state as BaseState<T>;
            CurrentState.EnterState();
        }

        /// <summary>
        /// Enters a parallel state. Parallel states are incredibly useful for running states alongside the current state.
        /// <para>For example, running an animation state while an NPC's movement state is running.</para>
        /// </summary>
        /// <param name="state">The parallel state to enter</param>
        public void EnterParallelState(BaseState<T> state)
        {
            if (state.IsStateActive)
            {
                return;
            }

            state.EnterState();
            ParallelStates.Add(state);
        }
        /// <summary>
        /// Exits a parallel state.
        /// </summary>
        /// <param name="state">The parallel state to exit.</param>
        public void ExitParallelState(BaseState<T> state)
        {
            if (!state.IsStateActive)
            {
                return;
            }

            state.ExitState();
            ParallelStates.Remove(state);
        }
    }
}
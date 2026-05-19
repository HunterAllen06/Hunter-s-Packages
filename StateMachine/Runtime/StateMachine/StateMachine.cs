using System;
using UnityEngine;

namespace HunterAllen.StateMachine
{
    /// <summary>
    /// Controls and manages the flow and transitions of two or more states.
    /// </summary>
    public abstract class StateMachine<T> : MonoBehaviour, IStateMachine<T>
    {
        public StateMachineBase<T> StateMachineBase { get; set; }

        #region Initialization
        void Awake()
        {
            StateMachineBase = new StateMachineBase<T>();

            OnInitialize();
        }
        /// <summary>
        /// This is where all states should be initialized.
        /// </summary>
        protected abstract void OnInitialize();
        #endregion

        #region Properties and Functions
        /// <summary>
        /// Check to see if a transition to a next state is available and update the current state.
        /// </summary>
        protected virtual void Update() => StateMachineBase.Update();
        /// <summary>
        /// Call fixed update on the current state and active parallel states.
        /// </summary>
        protected virtual void FixedUpdate() => StateMachineBase.FixedUpdate();

        /// <summary>
        /// Adds a transition that can occur during any state.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition on which to transition to the given state.</param>
        /// <param name="onTransition">An optional event delegate to invoke upon transition.</param>
        public virtual void AddGlobalTransition(IState to, ICondition condition, Action onTransition = null) => StateMachineBase.AddGlobalTransition(to, condition, onTransition);

        /// <summary>
        /// Checks all available transitions and returns one if its condition is met.
        /// </summary>
        /// <returns>The first transition that has a condition that returns true.</returns>
        protected virtual ITransition GetTransition() => StateMachineBase.GetTransition();

        /// <summary>
        /// Exits the current state if one is active, and enters the next state.
        /// <para>If you want to set the current state without a transition, use SetCurrentState(IState state) instead.</para>
        /// </summary>
        /// <param name="transition">The transition to use.</param>
        public void TransitionToState(ITransition transition) => StateMachineBase.TransitionToState(transition);
        /// <summary>
        /// Exits the current state if one is active, and enters the next state.
        /// </summary>
        /// <param name="state">The state to enter.</param>
        public void SetCurrentState(IState state) => StateMachineBase.SetCurrentState(state);

        /// <summary>
        /// Enters a parallel state. Parallel states are incredibly useful for running states alongside the current state.
        /// <para>For example, running an animation state while an NPC's movement state is running.</para>
        /// </summary>
        /// <param name="state">The parallel state to enter</param>
        public void EnterParallelState(BaseState<T> state) => StateMachineBase.EnterParallelState(state);
        /// <summary>
        /// Exits a parallel state.
        /// </summary>
        /// <param name="state">The parallel state to exit.</param>
        public void ExitParallelState(BaseState<T> state) => StateMachineBase.ExitParallelState(state);
        #endregion
    }
}
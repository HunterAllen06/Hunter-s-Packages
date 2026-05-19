namespace HunterAllen.StateMachine
{
    public interface IStateMachine<T>
    {
        public StateMachineBase<T> StateMachineBase { get; set; }
    }
}
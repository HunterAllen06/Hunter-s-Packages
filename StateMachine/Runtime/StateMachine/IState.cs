namespace HunterAllen.StateMachine
{
    public interface IState
    {
        public abstract void EnterState();
        public abstract void OnEnterState();
        public abstract void OnUpdateState();
        public abstract void OnFixedUpdateState();
        public abstract void ExitState();
        public abstract void OnExitState();
    }
}
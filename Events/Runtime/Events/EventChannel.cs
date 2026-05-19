using System;

namespace HunterAllen.Events
{
    public class EventChannel : IEvent
    {
        public void Raise() => EventManager.Raise(this);
        public void Bind(Action a) => EventManager.Bind(this, a);
        public void Unbind(Action a) => EventManager.Unbind(this, a);
    }
    public class EventChannel<T> : IEvent
    {
        public void Raise(T t) => EventManager.Raise(this, t);
        public void Bind(Action<T> a) => EventManager.Bind(this, a);
        public void Unbind(Action<T> a) => EventManager.Unbind(this, a);
    }

    /// <summary>
    /// Meant for holding two or more arguments for event bindings.
    /// </summary>
    public struct ArgsContainer<T1, T2>
    {
        public readonly T1 Arg1;
        public readonly T2 Arg2;

        public ArgsContainer(T1 _t1, T2 _t2)
        {
            Arg1 = _t1;
            Arg2 = _t2;
        }
    }
    /// <summary>
    /// Meant for holding two or more arguments for event bindings.
    /// </summary>
    public struct ArgsContainer<T1, T2, T3>
    {
        public readonly T1 Arg1;
        public readonly T2 Arg2;
        public readonly T3 Arg3;
        public ArgsContainer(T1 _t1, T2 _t2, T3 _t3)
        {
            Arg1 = _t1;
            Arg2 = _t2;
            Arg3 = _t3;
        }
    }
    /// <summary>
    /// Meant for holding two or more arguments for event bindings.
    /// </summary>
    public struct ArgsContainer<T1, T2, T3, T4>
    {
        public readonly T1 Arg1;
        public readonly T2 Arg2;
        public readonly T3 Arg3;
        public readonly T4 Arg4;

        public ArgsContainer(T1 _t1, T2 _t2, T3 _t3, T4 _t4)
        {
            Arg1 = _t1;
            Arg2 = _t2;
            Arg3 = _t3;
            Arg4 = _t4;
        }
    }
}
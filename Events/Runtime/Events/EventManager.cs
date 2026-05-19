using System;
using System.Collections.Generic;
using UnityEngine;

namespace HunterAllen.Events
{
    public static class EventManager
    {
        static Dictionary<object, Delegate> _events = new();

        #region Instance Events
        /// <summary>
        /// Binds an Action to an Event instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <param name="a">The action.</param>
        public static void Bind(this object e, Action a)
        {
            if (_events.TryGetValue(e, out var current))
            {
                _events[e] = Delegate.Combine(a, current);
                return;
            }

            _events[e] = a;
        }
        /// <summary>
        /// Binds an Action to an Event instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="T1">The type of the event instance.</typeparam>
        public static void Bind<T>(this object e, Action<T> a)
        {
            if (_events.TryGetValue(e, out var current))
            {
                _events[e] = Delegate.Combine(a, current);
                return;
            }

            _events[e] = a;
        }
        /// <summary>
        /// Unbinds an Action from an Event instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <param name="a">The action.</param>
        public static void Unbind(this object e, Action a)
        {
            if (!_events.TryGetValue(e, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[e] = current;
                return;
            }

            _events.Remove(e);
        }
        /// <summary>
        /// Unbinds an Action from an Event instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="T1">The type of the event instance.</typeparam>
        public static void Unbind<T>(this object e, Action<T> a)
        {
            if (!_events.TryGetValue(e, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[e] = current;
                return;
            }

            _events.Remove(e);
        }
        /// <summary>
        /// Raises an Event via instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        public static void Raise(this object e)
        {
            if (_events.TryGetValue(e, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event via instance.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <typeparam name="T1">The type of the event instance.</typeparam>
        public static void Raise<T>(this object e, T arg)
        {
            if (_events.TryGetValue(e, out var a))
            {
                ((Action<T>)a)?.Invoke(arg);
            }
        }
        #endregion

        #region Type Events
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TBind<T>(Action a) where T : IEvent
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TBind<T>(this T e, Action a) where T : IEvent
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TBind<T>(this object e, Action<T> a) where T : IEvent
        {
            var type = e.GetType();

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TBind<T1, T2>(Action<T2> a) where T1 : IEvent
        {
            var type = typeof(T1);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TUnbind<T>(Action a) where T : IEvent
        {
            var type = typeof(T);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TUnbind<T>(this T e, Action a) where T : IEvent
        {
            var type = typeof(T);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TUnbind<T>(this object e, Action<T> a) where T : IEvent
        {
            var type = e.GetType();

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        public static void TUnbind<T1, T2>(Action<T2> a) where T1 : IEvent
        {
            var type = typeof(T1);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        public static void TRaise<T>() where T : IEvent
        {
            var type = typeof(T);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        public static void TRaise<T>(this T e) where T : IEvent
        {
            var type = typeof(T);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <param name="e">The instance of the event.</param>
        /// <typeparam name="T1">The type of the event instance.</typeparam>
        public static void TRaise<T1, T2>(this T1 e, T2 arg) where T1 : IEvent
        {
            var type = typeof(T1);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action<T2>)a)?.Invoke(arg);
            }
        }
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <typeparam name="T1">The type of the event instance.</typeparam>
        public static void TRaise<T1, T2>(T2 arg) where T1 : IEvent
        {
            var type = typeof(T1);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action<T2>)a)?.Invoke(arg);
            }
        }
        #endregion

        #region String Events
        /*
        /// <summary>
        /// Binds an Action to an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SBind(string s, Action a)
        {
            if (_events.TryGetValue(s, out var current))
            {
                _events[s] = Delegate.Combine(a, current);
                return;
            }

            _events[s] = a;
        }
        /// <summary>
        /// Binds an Action to an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SBind<T>(string s, Action<T> a)
        {
            if (_events.TryGetValue(s, out var current))
            {
                _events[s] = Delegate.Combine(a, current);
                return;
            }

            _events[s] = a;
        }
        /// <summary>
        /// Unbinds an Action from an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SUnbind(string s, Action a)
        {
            if (!_events.TryGetValue(s, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[s] = current;
                return;
            }

            _events.Remove(s);
        }
        /// <summary>
        /// Unbinds an Action from an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SUnbind<T>(string s, Action<T> a)
        {
            if (!_events.TryGetValue(s, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[s] = current;
                return;
            }

            _events.Remove(s);
        }
        /// <summary>
        /// Raises an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        public static void SRaise(string s)
        {
            if (_events.TryGetValue(s, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        public static void SRaise<T>(string s, T t)
        {
            if (_events.TryGetValue(s, out var a))
            {
                ((Action<T>)a)?.Invoke(t);
            }
        }
        */
        #endregion
    }
}
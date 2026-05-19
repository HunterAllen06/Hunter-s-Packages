using System;
using UnityEngine;

namespace HunterAllen.Events
{
    public abstract class GenericEventBus<T> : EventBusBase
    {
        // public event Action<T> OnRaiseEvent;

        [SerializeField] T defaultValue;

        public void RaiseEvent()
        {
            RaiseEvent(defaultValue);
        }

        public void RaiseEvent(T value)
        {
            if (_debugLogs)
            {
                Debug.Log($"[Event Bus] Event raised on {name} with value {value}.");
            }

            this.Raise(value);
            // OnRaiseEvent.Invoke(value);
        }
    }
}
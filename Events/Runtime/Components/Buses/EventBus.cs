using System;
using UnityEngine;

namespace HunterAllen.Events
{
    public abstract class EventBusBase : ScriptableObject, IEvent
    {
        [SerializeField] protected bool _debugLogs;
    }

    [CreateAssetMenu(fileName = "Event_Bus", menuName = "Event Busses/Event Bus")]
    public class EventBus : ScriptableObject, IEvent
    { 
        // public event Action OnRaiseEvent;
        [SerializeField] bool _debugLogs;

        public void RaiseEvent()
        {
            if (_debugLogs)
            {
                Debug.Log($"[Event Bus] Event raised on {name}.");
            }

            this.Raise();
            // OnRaiseEvent?.Invoke();
        }
    }
}
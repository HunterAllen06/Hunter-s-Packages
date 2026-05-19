using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HunterAllen.Events
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] EventBus _eventBus;
        [SerializeField] UnityEvent _onRaiseEvent;

        void OnEnable()
        {
            _eventBus.Bind(OnRaiseEvent);
            // _eventBus.OnRaiseEvent += _onRaiseEvent.Invoke;
        }

        void OnDisable()
        {
            _eventBus.Unbind(OnRaiseEvent);
            // _eventBus.OnRaiseEvent -= _onRaiseEvent.Invoke;
        }

        void OnRaiseEvent() => _onRaiseEvent.Invoke();
    }
}
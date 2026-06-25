using UnityEngine;
using UnityEngine.Events;

namespace HunterAllen.Events
{
    public abstract class GenericEventListener<T> : MonoBehaviour
    {
        protected abstract GenericEventBus<T> _genericEventBus { get; set; }
        [SerializeField] UnityEvent<T> _onRaiseEvent;

        protected virtual void OnEnable()
        {
            _genericEventBus.Bind<T>(OnRaiseEvent);
            // _genericEventBus.OnRaiseEvent += _onRaiseEvent.Invoke;
        }

        protected virtual void OnDisable()
        {
            _genericEventBus.Unbind<T>(OnRaiseEvent);
            // _genericEventBus.OnRaiseEvent -= _onRaiseEvent.Invoke;
        }

        void OnRaiseEvent(T t)
        {
            _onRaiseEvent.Invoke(t);
        }
    }
}
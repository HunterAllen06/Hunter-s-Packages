using UnityEngine;

namespace HunterAllen.Events
{
    public class FloatEventListener : GenericEventListener<float>
    {
        [SerializeField] FloatEventBus _eventBus;
        protected override GenericEventBus<float> _genericEventBus { get => _eventBus; set => _genericEventBus = value; }
    }
}
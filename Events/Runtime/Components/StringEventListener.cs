using UnityEngine;

namespace HunterAllen.Events
{
    public class StringEventListener : GenericEventListener<string>
    {
        [SerializeField] StringEventBus _eventBus;
        protected override GenericEventBus<string> _genericEventBus { get => _eventBus; set => _genericEventBus = value; }
    }
}
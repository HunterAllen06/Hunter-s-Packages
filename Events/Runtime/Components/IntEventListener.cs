using UnityEngine;

namespace HunterAllen.Events
{
    public class IntEventListener : GenericEventListener<int>
    {
        [SerializeField] IntEventBus _eventBus;
        protected override GenericEventBus<int> _genericEventBus { get => _eventBus as GenericEventBus<int>; set => _genericEventBus = value; }
    }
}
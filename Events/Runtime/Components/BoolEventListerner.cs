using UnityEngine;

namespace HunterAllen.Events
{
    public class BoolEventListener : GenericEventListener<bool>
    {
        [SerializeField] BoolEventBus _eventBus;
        protected override GenericEventBus<bool> _genericEventBus { get => _eventBus; set => _genericEventBus = value; }
    }
}
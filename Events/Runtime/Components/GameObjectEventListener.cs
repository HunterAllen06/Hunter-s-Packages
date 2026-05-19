using UnityEngine;

namespace HunterAllen.Events
{
    public class GameObjectEventListener : GenericEventListener<GameObject>
    {
        [SerializeField] GameObjectEventBus _eventBus;
        protected override GenericEventBus<GameObject> _genericEventBus { get => _eventBus; set => _genericEventBus = value; }
    }
}
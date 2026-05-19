using UnityEngine;

namespace HunterAllen.Events
{
    public class AudioEventListener : GenericEventListener<AudioClip>
    {
        [SerializeField] AudioEventBus _eventBus;
        protected override GenericEventBus<AudioClip> _genericEventBus { get => _eventBus; set => _genericEventBus = value; }
    }
}
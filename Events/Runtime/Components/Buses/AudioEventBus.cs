using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "AudioEvent_Bus", menuName = "Event Busses/Audio Event Bus")]
    public class AudioEventBus : GenericEventBus<AudioClip> {}
}
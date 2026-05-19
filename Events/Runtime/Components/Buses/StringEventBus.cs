using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "StringEvent_Bus", menuName = "Event Busses/String Event Bus")]
    public class StringEventBus : GenericEventBus<string> {}
}
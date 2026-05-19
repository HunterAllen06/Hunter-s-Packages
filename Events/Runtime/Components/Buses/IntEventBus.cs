using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "IntEvent_Bus", menuName = "Event Busses/Int Event Bus")]
    public class IntEventBus : GenericEventBus<int> {}
}
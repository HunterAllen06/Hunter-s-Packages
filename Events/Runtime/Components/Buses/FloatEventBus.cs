using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "FloatEvent_Bus", menuName = "Event Busses/Float Event Bus")]
    public class FloatEventBus : GenericEventBus<float> {}
}
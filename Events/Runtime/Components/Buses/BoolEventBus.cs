using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "Bool", menuName = "Event Busses/Bool Event Bus")]
    public class BoolEventBus : GenericEventBus<bool> { }
}
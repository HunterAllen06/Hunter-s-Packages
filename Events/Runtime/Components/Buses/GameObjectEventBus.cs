using UnityEngine;

namespace HunterAllen.Events
{
    [CreateAssetMenu(fileName = "GameObjectEvent_Bus", menuName = "Event Busses/GameObject Event Bus")]
    public class GameObjectEventBus : GenericEventBus<GameObject> {}
}
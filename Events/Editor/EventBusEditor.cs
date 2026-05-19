using UnityEditor;
using UnityEngine;

namespace HunterAllen.Events.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EventBus))]
    public class EventBusEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EventBus eventBus = (EventBus)target;

            if (GUILayout.Button("Raise Event"))
            {
                eventBus.RaiseEvent();
            }
        }
    }
#endif
}
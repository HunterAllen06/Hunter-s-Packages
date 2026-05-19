using System;
using UnityEditor;
using UnityEngine;

namespace HunterAllen.Events.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EventBusBase), true)]
    public class GenericEventBusEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (target is EventBusBase eventBus)
            {
                if (GUILayout.Button("Raise Event"))
                {
                    var method = eventBus.GetType().GetMethod("RaiseEvent", Type.EmptyTypes);
                    method?.Invoke(eventBus, null);
                }
            }
        }
    }
#endif
}
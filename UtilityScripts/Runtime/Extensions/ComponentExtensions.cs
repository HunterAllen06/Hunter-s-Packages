using UnityEngine;

namespace HunterAllen.Utility
{
    public static class ComponentExtensions
    {
        public static T GetOrAdd<T>(this GameObject obj) where T : Component => obj.GetComponent<T>() ?? obj.AddComponent<T>();

        public static T OrNull<T>(this T t) where T : Object => t ? t : null;
    }
}
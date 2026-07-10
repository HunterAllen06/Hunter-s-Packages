using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject self) where T : MonoBehaviour
        {
            return self.GetComponent<T>() ?? self.AddComponent<T>();
        }
        public static T GetOrAddComponent<T>(this GameObject self) where T : Component
        {
            self.TryGetComponent(out T t);
            t ??= self.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// A version of GetComponent<T> that isn't exclusive to GameObjects/Components. Should work on interfaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetComponent<T>(this object self) where T : class
        {
            return (self as Component)?.GetComponent<T>();
        }
        /// <summary>
        /// A version of GetComponents<T> that isn't exclusive to GameObjects/Components. Should work on interfaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static List<T> GetComponents<T>(this object self) where T : class
        {
            return (self as Component)?.GetComponents<T>().ToList();
        }

        public static bool TryGetComponent<T>(this object self, out T t) where T : class
        {
            if (self == null)
            {
                t = null;
                return false;
            }
            if ((self as Component) == null)
            {
                t = null;
                return false;
            }

            return (self as Component).TryGetComponent(out t);
        }
        public static bool TryGetComponentInParent<T>(this Component self, out T component) where T : Component
        {
            component = self.GetComponentInParent<T>();
            return component != null;
        }
        public static bool TryGetComponentInParent<T>(this GameObject self, out T component) where T : Component
        {
            component = self.GetComponentInParent<T>();
            return component != null;
        }
        public static bool TryGetComponentInChildren<T>(this Component self, out T component) where T : Component
        {
            component = self.GetComponentInChildren<T>();
            return component != null;
        }
        public static bool TryGetComponentInChildren<T>(this GameObject self, out T component) where T : Component
        {
            component = self.GetComponentInChildren<T>();
            return component != null;
        }
        public static bool TryGetComponentInRelatives<T>(this Component self, out T component) where T : Component
        {
            component = self.GetComponentInChildren<T>()??self.GetComponentInParent<T>();
            return component != null;
        }
        public static bool TryGetComponentInRelatives<T>(this GameObject self, out T component) where T : Component
        {
            component = self.GetComponentInChildren<T>()??self.GetComponentInParent<T>();
            return component != null;
        }
        
        /// <summary>
        /// Compares the layer on a GameObject to a LayerMask
        /// </summary>
        /// <returns>True if they are equal</returns>
        public static bool CompareLayer(LayerMask layerMask, GameObject obj)
        {
            return (layerMask.value & (1 << obj.layer)) != 0;
        }
        /// <summary>
        /// Compares the layer on this GameObject to a LayerMask
        /// </summary>
        /// <returns>True if they are equal</returns>
        public static bool CompareLayer(this GameObject obj, LayerMask layerMask)
        {
            return (layerMask.value & (1 << obj.layer)) != 0;
        }
    }
}
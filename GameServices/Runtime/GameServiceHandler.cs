using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;

namespace HunterAllen.GameServices
{
    public abstract class GameServiceHandler<J>
    {
        protected static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service;
            Debug.Log($"<color=#8ff>[{typeof(J).Name}] Registered {typeof(T).Name}.</color>", service as GameObject);
            // Debug.Log($"<color=#8ff>[{typeof(J).Name}] Registered {_services[typeof(T)]} as {typeof(T).Name}.</color>", service as GameObject);
        }
        public static void Deregister<T>(T service)
        {
            Debug.Log($"<color=#8ff>[{typeof(J).Name}] Deregistered {typeof(T).Name}.</color>", service as GameObject);
            // Debug.Log($"<color=#8ff>[{typeof(J).Name}] Deregistered {_services[typeof(T)]} as {typeof(T).Name}.</color>", service as GameObject);
            _services.Remove(typeof(T));
        }

        public static T Get<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                throw new NullReferenceException($"{typeof(J).Name} does not have a service of type {typeof(T).Name} in its registry! Did you forget to register the service in the GameBootstrapper class?");
            }
            if (_services[typeof(T)] == null)
            {
                throw new NullReferenceException($"{typeof(T).Name} was found in the {typeof(J).Name} registry but its value is null! Did it get destroyed?");
            }

            return (T)_services[typeof(T)];
        }
        public static bool TryGet<T>(out T service)
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                service = default;
                return false;
            }
            if (_services[typeof(T)] == null)
            {
                service = default;
                return false;
            }

            service = (T)_services[typeof(T)];
            return true;
        }
        public static async Task<T> GetAsync<T>()
        {
            while (!Contains<T>())
            {
                if (Application.exitCancellationToken.IsCancellationRequested)
                {
                    return default;
                }

                await Task.Yield();
            }

            return (T)_services[typeof(T)];
        }
        
        public static bool Contains<T>() => _services.ContainsKey(typeof(T));
    }
}
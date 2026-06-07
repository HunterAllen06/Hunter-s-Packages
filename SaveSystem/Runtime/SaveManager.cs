using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace HunterAllen.SaveSystem
{
    public abstract class SaveManager
    {
        public static Dictionary<Type, object> Data = new();

        static IDataHandler _dataHandler;
        static bool _hasInitialized;

        /// <summary>
        /// Called as soon as the game starts via GameBootstrapper.cs
        /// </summary>
        protected virtual void Initialize()
        {
            if (_hasInitialized)
            {
                Debug.Log("SaveManager tried initializing more than once, skipping initialization.");
                return;
            }

            _dataHandler = new FileDataHandler(Application.persistentDataPath);

            _hasInitialized = true;
        }

        public void New<T>(T t)
        {
            Data[typeof(T)] = t;
            Save(t);
            Debug.Log($"Created new {typeof(T).Name}.");
        }

        public void Save<T>() => Save(Get<T>());
        public void Save<T>(T data)
        {
            // Save data
            _dataHandler.Save(data, GetDataFileName<T>());
        }

        public T Load<T>()
        {
            // Load data
            T data = _dataHandler.Load<T>(GetDataFileName<T>());

            if (data == null)
            {
                Debug.LogWarning($"No {typeof(T).Name} found, initial data needs to be created.");
                return default;
            }

            // Push data
            Data[typeof(T)] = data;

            return data;
        }

        public static T Get<T>()
        {
            if (!Data.ContainsKey(typeof(T)))
            {
                Debug.LogError($"SaveManager does not contain data of type {typeof(T).Name}");
                return default;
            }
            return (T)Data[typeof(T)];
        }

        public abstract string GetDataFileName<T>();
    }
}
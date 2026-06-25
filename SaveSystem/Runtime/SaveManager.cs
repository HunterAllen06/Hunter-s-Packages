using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace HunterAllen.SaveSystem
{
    public static class SaveManager
    {
        public static Dictionary<Type, object> Data = new();

        static IDataHandler _dataHandler;
        static bool _hasInitialized;

        /// <summary>
        /// Called as soon as the game starts via GameBootstrapper.cs
        /// </summary>
        public static void Initialize()
        {
            if (_hasInitialized)
            {
                Debug.Log("SaveManager tried initializing more than once, skipping initialization.");
                return;
            }

            _dataHandler = new FileDataHandler(Application.persistentDataPath);

            _hasInitialized = true;
        }

        public static void New<T>(T t, string fileName)
        {
            Data[typeof(T)] = t;
            Save(t, fileName);
            Debug.Log($"Created new {typeof(T).Name}.");
        }

        public static void Save<T>(string fileName) => Save(Get<T>(), fileName);
        public static void Save<T>(T data, string fileName)
        {
            // Save data
            _dataHandler.Save(data, fileName);
        }

        public static T Load<T>(string fileName)
        {
            // Load data
            T data = _dataHandler.Load<T>(fileName);

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
    }
}
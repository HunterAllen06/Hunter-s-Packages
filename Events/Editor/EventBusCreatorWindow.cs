using UnityEngine;
using UnityEditor;
using HunterAllen.Events;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

public class EventBusCreatorWindow : EditorWindow
{
    string _assetName = "OnEvent_Bus";
    string _assetPath = "Assets/_Project/ScriptableObjects/EventBuses";
    int _selectedFolderIndex = 0;
    int _previousFolderIndex = 0;
    string _eventBusType = "Normal";
    int _selectedEventBusType = 0;
    
    string _currentPath = "Assets/_Project/ScriptableObjects/EventBuses";
    List<string> _relativeFolders = new List<string>();
    List<string> _folderPaths = new List<string>(); // Stores full Unity paths

    List<string> _eventBusTypes = new List<string>()
    {
        "Normal",
        "Int",
        "Float",
        "String",
        "Bool",
        "GameObject",
        "Audio",
    };

    string _previousAssetPath;

    [MenuItem("Tools/Create EventBus")]
    public static void ShowWindow()
    {
        GetWindow<EventBusCreatorWindow>("EventBus Creator");
    }

    [MenuItem("Assets/Create EventBus")]
    public static void ShowWindowForAssets()
    {
        GetWindow<EventBusCreatorWindow>("EventBus Creator");
    }

    void OnEnable()
    {
        _assetPath = EditorPrefs.HasKey("EVENT_BUSES_PATH") ? EditorPrefs.GetString("EVENT_BUSES_PATH") : "Assets/_Project/ScriptableObjects/EventBuses";
        _currentPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        _selectedFolderIndex = EditorPrefs.HasKey("EVENT_BUSES_PATHINDEX") ? EditorPrefs.GetInt("EVENT_BUSES_PATHINDEX") : 0;

        var asset = _currentPath.Split("/").FirstOrDefault(x => x.Contains(".asset"));
        if (!string.IsNullOrEmpty(asset))
        {
            var length = asset.Length + 1;
            _currentPath = _currentPath.Remove(_currentPath.Length - length, length);
        }

        RefreshFolderList();
    }

    void OnGUI()
    {
        GUILayout.Label("Create EventBus", EditorStyles.boldLabel);

        _assetName = EditorGUILayout.TextField("Asset Name", _assetName);

        _previousAssetPath = _assetPath;
        _assetPath = EditorGUILayout.TextField("Asset Path", _assetPath);
        
        if (_assetPath != _previousAssetPath)
        {
            EditorPrefs.SetString("EVENT_BUSES_PATH", _assetPath);
        }
        
        if (_relativeFolders.Count == 0)
        {
            EditorGUILayout.HelpBox($"No folders found under {_assetPath}.", MessageType.Warning);
            if (GUILayout.Button("Refresh Folders"))
            {
                RefreshFolderList();
            }
                
            return;
        }

        _previousFolderIndex = _selectedFolderIndex;
        _selectedFolderIndex = EditorGUILayout.Popup("Target Folder", _selectedFolderIndex, _relativeFolders.ToArray());

        if (_selectedFolderIndex != _previousFolderIndex)
        {
            EditorPrefs.SetInt("EVENT_BUSES_PATHINDEX", _selectedFolderIndex);
            _currentPath = _folderPaths[_selectedFolderIndex];
        }

        _selectedEventBusType = EditorGUILayout.Popup("EventBus Type", _selectedEventBusType, _eventBusTypes.ToArray());

        if (GUILayout.Button("Create EventBus"))
        {
            CreateEventBusAsset();
        }
    }

    void RefreshFolderList()
    {
        _relativeFolders.Clear();
        _folderPaths.Clear();

        if (!AssetDatabase.IsValidFolder(_assetPath))
            return;

        var fullFolders = Directory.GetDirectories(_assetPath, "*", SearchOption.AllDirectories)
            .Select(f => f.Replace('\\', '/'))
            .ToList();

        fullFolders.Insert(0, _assetPath); // Include the root folder first

        foreach (string path in fullFolders)
        {
            _folderPaths.Add(path);

            string relative = path == _assetPath
                ? "(Root)"
                : path.Replace(_assetPath + "/", ""); // Remove the root path prefix

            _relativeFolders.Add(relative);
        }
        
        if (!string.IsNullOrEmpty(_currentPath))
        {
            for (int i = 0; i < _relativeFolders.Count; i++)
            {
                if (_currentPath == $"{_assetPath}/{_relativeFolders[i]}")
                {
                    _selectedFolderIndex = i;
                    break;
                }
            }
        }
    }

    void CreateEventBusAsset()
    {
        string folderPath = _currentPath;
        string fullPath = Path.Combine(folderPath, _assetName + ".asset");

        Type t = typeof(EventBus);

        switch (_eventBusType)
        {
            case "Normal":
                t = typeof(EventBus);
                break;
            case "Int":
                t = typeof(IntEventBus);
                break;
            case "Float":
                t = typeof(FloatEventBus);
                break;
            case "String":
                t = typeof(StringEventBus);
                break;
            case "Bool":
                t = typeof(BoolEventBus);
                break;
            case "GameObject":
                t = typeof(GameObjectEventBus);
                break;
            case "Audio":
                t = typeof(AudioEventBus);
                break;
        }
        
        if (AssetDatabase.LoadAssetAtPath(fullPath, t) != null)
        {
            EditorUtility.DisplayDialog("Error", "An EventBus with that name already exists in this folder.", "OK");
            return;
        }

        var newBus = ScriptableObject.CreateInstance(t);
        AssetDatabase.CreateAsset(newBus, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Undo.RecordObject(newBus, "Undo EventBus creation");
        Selection.activeObject = newBus;

        Close();
    }
}
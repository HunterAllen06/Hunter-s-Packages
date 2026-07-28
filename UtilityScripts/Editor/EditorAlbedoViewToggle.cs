#if HAS_URP && UNITY_6000_0_OR_NEWER
using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine.Rendering.Universal;

namespace HunterAllen.Editor
{   
    [Overlay(typeof(SceneView), "Albedo View Toggle", true)]
    public class AlbedoViewToggleOverlay : ToolbarOverlay
    {
        const string _toolbarElementName = "Editor Utility/Albedo View Toggle";
        static string _iconName => UniversalRenderPipelineDebugDisplaySettings.Instance.materialSettings.materialDebugMode == DebugMaterialMode.None ? "Lighting" : "SceneViewLighting";

        [MainToolbarElement(_toolbarElementName, defaultDockPosition = MainToolbarDockPosition.Middle)]
        static MainToolbarElement CreateBar()
        {
            var icon = EditorGUIUtility.IconContent(_iconName).image as Texture2D;
            return new MainToolbarButton(new MainToolbarContent(icon, "Albedo View Toggle"), OnClick);
        }

        public static void OnClick()
        {
            var settings = UniversalRenderPipelineDebugDisplaySettings.Instance;
            if (settings.materialSettings.materialDebugMode == DebugMaterialMode.Albedo)
            {
                // Lit Mode
                settings.materialSettings.materialDebugMode = DebugMaterialMode.None;
            }
            else
            {
                // Albedo Mode
                settings.materialSettings.materialDebugMode = DebugMaterialMode.Albedo;
            }

            MainToolbar.Refresh(_toolbarElementName);
            AlbedoViewToggleButton.SetIcon(_iconName);
        }

        AlbedoViewToggleOverlay() : base(AlbedoViewToggleButton.Id) { }

        [EditorToolbarElement(Id, typeof(SceneView))]
        class AlbedoViewToggleButton : EditorToolbarButton
        {
            public const string Id = "Albedo View Toggle";
            public static AlbedoViewToggleButton Instance;

            public AlbedoViewToggleButton()
            {
                Instance = this;
                SetIcon(_iconName);
                clicked += OnClick;
            }

            public static void SetIcon(string name) => Instance.icon = EditorGUIUtility.FindTexture(name);
        }
    }
}
#endif
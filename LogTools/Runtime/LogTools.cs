using System.Runtime.CompilerServices;
using UnityEngine;

namespace HunterAllen.LogTools
{
    public static class LogTools
    {
        public static void Log(this object self, object message)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.Log($"[{self.GetType().Name}] {message}", self as MonoBehaviour);
            }
        }
        public static void LogWarning(this object self, object message)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.LogWarning($"[{self.GetType().Name}] {message}", self as MonoBehaviour);
            }
        }
        public static void LogError(this object self, object message)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.LogError($"[{self.GetType().Name}] {message}", self as MonoBehaviour);
            }
        }
        public static void Log(this object self, object message, string color)
        {
            Log(self, $"<color={color}>{message}</color>");
        }
        public static void LogWarning(this object self, object message, string color)
        {
            LogWarning(self, $"<color={color}>{message}</color>");
        }
        public static void LogError(this object self, object message, string color)
        {
            LogError(self, $"<color={color}>{message}</color>");
        }

        /// <summary>
        /// Calls Log() like normal but adds caller info in the
        /// </summary>
        public static void LogC(this object self, object message, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.Log($"[{self.GetType().Name}.cs:<a href=\"{path}\" line=\"{line}\">{line}</a> - {member}] {message}", self as MonoBehaviour);
            }
        }
        /// <summary>
        /// Calls LogWarning() like normal but adds caller info in the
        /// </summary>
        public static void LogWarningC(this object self, object message, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.LogWarning($"[{self.GetType().Name}.cs:<a href=\"{path}\" line=\"{line}\">{line}</a> - {member}] {message}", self as MonoBehaviour);
            }
        }
        /// <summary>
        /// Calls LogError() like normal but adds caller info in the
        /// </summary>
        public static void LogErrorC(this object self, object message, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            if (Application.installMode == ApplicationInstallMode.DeveloperBuild || Application.installMode == ApplicationInstallMode.Editor)
            {
                Debug.LogError($"[{self.GetType().Name}.cs:<a href=\"{path}\" line=\"{line}\">{line}</a> - {member}] {message}", self as MonoBehaviour);
            }
        }
        /// <summary>
        /// Calls Log() like normal but adds caller info in the
        /// </summary>
        public static void LogC(this object self, object message, string color, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            LogC(self, $"<color={color}>{message}</color>");
        }
        /// <summary>
        /// Calls LogWarning() like normal but adds caller info in the
        /// </summary>
        public static void LogWarningC(this object self, object message, string color, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            LogWarningC(self, $"<color={color}>{message}</color>");
        }
        /// <summary>
        /// Calls LogError() like normal but adds caller info in the
        /// </summary>
        public static void LogErrorC(this object self, object message, string color, [CallerFilePath] string path = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            LogErrorC(self, $"<color={color}>{message}</color>");
        }
    }
}
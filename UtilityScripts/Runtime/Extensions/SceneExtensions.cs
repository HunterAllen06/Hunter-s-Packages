using System.Linq;
using UnityEngine.SceneManagement;

namespace HunterAllen.Utility
{
    public static class SceneExtensions
    {
        public static string GetSceneNameByBuildIndex(int index) => SceneUtility.GetScenePathByBuildIndex(index).Split('/').LastOrDefault().Split('.')[0];
    }
}
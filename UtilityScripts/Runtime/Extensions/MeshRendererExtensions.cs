using UnityEngine;

namespace HunterAllen.Utility
{
    public static class MeshRendererExtensions
    {
        public static float GetShapeKeyValue(this SkinnedMeshRenderer self, string shapeName)
        {
            return self.GetBlendShapeWeight(self.sharedMesh.GetBlendShapeIndex(shapeName));
        }
    }
}
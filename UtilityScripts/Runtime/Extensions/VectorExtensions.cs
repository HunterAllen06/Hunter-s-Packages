using UnityEngine;

namespace HunterAllen.Utility
{
    public static class VectorExtensions
    {
        public static Vector3 SetX(this Vector3 v, float x) => new(x, v.y, v.z);
        public static Vector3 SetY(this Vector3 v, float y) => new(v.x, y, v.z);
        public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);

        /// <summary>
        /// Clamps this vector as if it were an euler, keeping all angles between -180 and 180 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 ClampAsEuler(this Vector3 vector)
        {
            while (Mathf.Abs(vector.x) > 180f || Mathf.Abs(vector.y) > 180f || Mathf.Abs(vector.z) > 180f)
            {
                vector.x = vector.x > 180f ? vector.x - 360f : vector.x < -180f ? vector.x + 360 : vector.x;
                vector.y = vector.y > 180f ? vector.y - 360f : vector.y < -180f ? vector.y + 360 : vector.y;
                vector.z = vector.z > 180f ? vector.z - 360f : vector.z < -180f ? vector.z + 360 : vector.z;
            }
            return vector;
        }
    }
}
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class Spring
    {
        public static float GetSpringForce(float current, float target, float currentForce, float springForce, float damperForce)
        {
            return (target - current) * springForce - (currentForce * damperForce);
        }
    
        public static Vector3 GetSpringVector(Vector3 current, Vector3 target, Vector3 currentForce, float springForce, float damperForce)
        {
            return ((target - current) * springForce) - (currentForce * damperForce);
        }

        public static Vector3 GetSpringVector(Vector3 difference, Vector3 currentForce, float springForce, float damperForce)
        {
            return (difference * springForce) - (currentForce * damperForce);
        }
    }
}
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class RigidbodyExtensions
    {
        public static bool IsMoving(this Rigidbody rb)
        {
            #if UNITY_6000_0_OR_NEWER
            return rb.linearVelocity.magnitude >= 0.001f;
            #else
            return rb.velocity.magnitude >= 0.001f;
            #endif
        }
    }
}
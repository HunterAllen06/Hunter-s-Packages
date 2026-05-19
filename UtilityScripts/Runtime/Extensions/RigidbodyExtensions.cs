using UnityEngine;

namespace HunterAllen.Utility
{
    public static class RigidbodyExtensions
    {
        public static bool IsMoving(this Rigidbody rb)
        {
            return rb.velocity.magnitude >= 0.001f;
        }
    }
}
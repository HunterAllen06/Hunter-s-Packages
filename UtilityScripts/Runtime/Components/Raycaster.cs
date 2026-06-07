using UnityEngine;

namespace HunterAllen.Utility
{
    public class Raycaster : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("If <= 0, raycast distance will be infinity.")]
        float _distance;

        [SerializeField]
        LayerMask _mask;

        float _distanceAdjusted => _distance <= 0 ? float.MaxValue : _distance;
        
        public bool Raycast()
        {
            return Physics.Raycast(transform.position, transform.forward, _distanceAdjusted, _mask);
        }
        public bool Raycast(LayerMask mask)
        {
            return Physics.Raycast(transform.position, transform.forward, _distanceAdjusted, mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction)
        {
            return Physics.Raycast(origin, direction, _distanceAdjusted, _mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, LayerMask mask)
        {
            return Physics.Raycast(origin, direction, _distanceAdjusted, mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue)
        {
            return Physics.Raycast(origin, direction, distance, _mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, float distance, LayerMask mask)
        {
            return Physics.Raycast(origin, direction, distance, mask);
        }

        public bool Raycast(out RaycastHit hit)
        {
            return Physics.Raycast(transform.position, transform.forward, out hit, _distanceAdjusted, _mask);
        }
        public bool Raycast(out RaycastHit hit, LayerMask mask)
        {
            return Physics.Raycast(transform.position, transform.forward, out hit, _distanceAdjusted, mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit)
        {
            return Physics.Raycast(origin, direction, out hit, _distanceAdjusted, _mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, LayerMask mask)
        {
            return Physics.Raycast(origin, direction, out hit, _distanceAdjusted, mask);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance = float.MaxValue)
        {
            return Physics.Raycast(origin, direction, out hit, distance);
        }
        public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance, LayerMask mask)
        {
            return Physics.Raycast(origin, direction, out hit, distance, mask);
        }
    }
}
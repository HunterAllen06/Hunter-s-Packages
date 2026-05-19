using UnityEngine;
using UnityEngine.AI;

namespace HunterAllen.Utility
{
    public static class NavMeshExtensions
    {
        static NavMeshPath _path;
        static NavMeshHit _hit;
        static Vector3[] _corners = new Vector3[32];
        /// <summary>
        /// Calculates a NavMeshAgent's distance to a target position without memory allocation.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public static float GetPathDistanceNonAlloc(this NavMeshAgent agent, Vector3 targetPosition)
        {
            if (!agent.isOnNavMesh)
            {
                return 1001f;
            }
            else if (!NavMesh.SamplePosition(targetPosition, out _hit, 0.05f, NavMesh.AllAreas))
            {
                return 1002f;
            }

            if (_path == null)
            {
                _path = new NavMeshPath();
            }
            else if (!agent.CalculatePath(_hit.position, _path) || _path.status != NavMeshPathStatus.PathComplete)
            {
                return 1003f;
            }

            float distance = 0f;
            int cornerBufferLegnth = _path.GetCornersNonAlloc(_corners);

            for (int i = 1; i < cornerBufferLegnth; i++)
            {
                distance += Vector3.Distance(_corners[i - 1], _corners[i]);
            }

            return distance;
        }
    }
}
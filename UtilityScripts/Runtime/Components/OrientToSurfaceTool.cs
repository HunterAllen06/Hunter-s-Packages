using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace HunterAllen.Utility
{
    public class OrientToSurfaceTool : MonoBehaviour
    {
        #if ODIN_INSPECTOR
        [HorizontalGroup("B", LabelWidth = 100f)]
        #endif
        [SerializeField]
        Direction _raycastDirection;

        #if ODIN_INSPECTOR
        [HorizontalGroup("B", LabelWidth = 90f)]
        #endif
        [SerializeField]
        Direction _outputDirection;

        #if ODIN_INSPECTOR
        [HorizontalGroup("B", LabelWidth = 65f)]
        #endif
        [SerializeField]
        LayerMask _layerMask;

        #if ODIN_INSPECTOR
        [HorizontalGroup("A")]
        [Button("Orient!", Icon = SdfIconType.ArrowUpRightCircle)]
        void Orient() => Orient(out RaycastHit hit);
        #else
        [ContextMenu("Orient!")]
        #endif
        bool Orient(out RaycastHit hit)
        {
            bool successful = Raycast(out hit);

            if (!successful)
            {
                return false;
            }

            Vector3 forward = hit.normal;

            switch (_outputDirection)
            {
                case Direction.XPositive:
                    transform.right = forward;
                    break;
                case Direction.XNegative:
                    transform.right = -forward;
                    break;
                case Direction.YPositive:
                    transform.up = forward;
                    break;
                case Direction.YNegative:
                    transform.up = -forward;
                    break;
                case Direction.ZPositive:
                    transform.forward = forward;
                    break;
                case Direction.ZNegative:
                    transform.forward = -forward;
                    break;
            }

            return true;
        }

        #if ODIN_INSPECTOR
        [HorizontalGroup("A")]
        [Button("Orient and Snap to Position!", Icon = SdfIconType.ArrowUpRightCircleFill)]
        #else
        [ContextMenu("Orient and Snap to Position!")]
        #endif
        void OrientAndSnap()
        {
            if (Orient(out RaycastHit hit))
            {
                transform.position = hit.point;
            }
        }

        bool Raycast(out RaycastHit hit)
        {
            return Physics.Raycast(transform.position, GetDirection(_raycastDirection), out hit, 5f, _layerMask);
        }
        Vector3 GetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.XPositive:
                    return transform.right;
                case Direction.XNegative:
                    return -transform.right;
                case Direction.YPositive:
                    return transform.up;
                case Direction.YNegative:
                    return -transform.up;
                case Direction.ZPositive:
                    return transform.forward;
                case Direction.ZNegative:
                    return -transform.forward;
                default:
                    return Vector3.zero;
            }
        }

        void OnDrawGizmos()
        {
            bool successful = Raycast(out RaycastHit hit);

            if (!successful)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, GetDirection(_raycastDirection) * 5f);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, hit.point);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(hit.point, hit.point + hit.normal * 0.3f);
            }
        }
    }
    enum Direction

    {
        [InspectorName("X+")]
        XPositive,
        [InspectorName("X-")]
        XNegative,
        [InspectorName("Y+")]
        YPositive,
        [InspectorName("Y-")]
        YNegative,
        [InspectorName("Z+")]
        ZPositive,
        [InspectorName("Z-")]
        ZNegative,
    }
}
using System.Linq;
using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        public Rigidbody Rigidbody;

        [SerializeField]
        public CapsuleCollider Collider;

        [SerializeField]
        Transform _orientation;

        [SerializeField]
        Transform _cameraTransform;

        [Header("Acceleration")]
        [SerializeField]
        float _accelerationStrength = 1.5f;

        [Header("Speed")]
        [SerializeField]
        float _defaultSpeed = 1.5f;

        [SerializeField]
        float _sprintSpeed = 3f;

        [SerializeField]
        float _crouchSpeedMultiplier = 0.5f;

        [Header("Spring")]
        [SerializeField]
        float _springForce = 1f;

        [SerializeField]
        float _springForceSprintMultiplier = 2f;

        [SerializeField]
        float _springDamp = 1f;

        [Space]
        [SerializeField]
        float _springHeight = 0.25f;

        [SerializeField]
        float _springRaycastDistance = 0.5f;

        [Range(0.01f, 1f)]
        [SerializeField]
        float _springRadius = 0.5f;
        
        [Range(0f, 1f)]
        [SerializeField]
        float _directionBias = 0.2f;
        
        [SerializeField]
        float _directionBiasSprintMultiplier = 2f;

        [SerializeField]
        LayerMask _groundLayer;

        [Header("Steps and Slopes")]
        [SerializeField]
        float _maxStepHeight = 0.5f;

        [Range(1f, 89f)]
        [SerializeField]
        float _maxSlopeAngle = 40f;
        
        [Header("Crouching")]
        [Range(0.1f, 3f)]
        [SerializeField]
        float _defaultHeight = 1.5f;

        [SerializeField]
        [Range(0.1f, 1f)]
        float _crouchHeight = 0.8f;

        [SerializeField]
        float _crouchSmoothSpeed = 8f;

        Vector2 _moveInput;
        bool _isSprinting;
        bool _isCrouching;
        bool _isAllowedToStand;
        float _maxSlopeDot;
        RaycastHit[] _groundHitResults = new RaycastHit[4];
        RaycastHit[] _headCheckResults = new RaycastHit[1];
        Ray _ray;

        void Update()
        {
            HandleCrouchInput();
        }

        void FixedUpdate()
        {
            ApplySpringForce();
            ApplyMovementForce(_moveInput);
        }

        void ApplySpringForce()
        {
#if UNITY_6000_0_OR_NEWER
            Vector3 velocity = Rigidbody.linearVelocity;
#else
            Vector3 velocity = _rigidbody.velocity;
#endif
            Vector3 direction = velocity;
            direction.y = 0;
            direction = direction.magnitude > 0.2f ? direction : Vector3.zero;

            float multiplier = _isSprinting ? _directionBiasSprintMultiplier : 1f;

            _ray = new(Collider.transform.position - (Collider.height * 0.5f - Collider.radius) * Vector3.up + _directionBias * multiplier * direction.normalized, Vector3.down);

            _groundHitResults = new RaycastHit[4];
            int hits = Physics.SphereCastNonAlloc(_ray, Collider.radius * _springRadius, _groundHitResults, _springRaycastDistance, _groundLayer, QueryTriggerInteraction.Ignore);
            if (hits == 0) return;

            RaycastHit hit = _groundHitResults.OrderBy(x => (new Vector2(x.point.x, x.point.z) - new Vector2(Collider.transform.position.x, Collider.transform.position.z)).magnitude).ToArray()[0];
            RaycastHit initialHit = hit;
            float feet = hit.collider ? hit.point.y : Collider.transform.position.y - Collider.height - _springHeight;

            for (int i = 0; i < hits; i++)
            {
                var h = _groundHitResults[i];

                if (h.collider != null &&
                    (hit.collider == null || h.distance < hit.distance) &&
                    h.point != Vector3.zero &&
                    h.point.y - feet < _maxStepHeight + 0.05f &&
                    Vector3.Dot(h.normal, Vector3.up) > _maxSlopeDot)
                {
                    hit = h;
                }
            }

            multiplier = _isSprinting ? _springForceSprintMultiplier : 1f;
            float force = (_springHeight - hit.distance) * _springForce * multiplier - (velocity.y * _springDamp);
            Rigidbody.AddForce(force * Time.fixedDeltaTime * Vector3.up);
        }
        void ApplyMovementForce(Vector2 input)
        {
            float targetSpeed = _isSprinting ? _sprintSpeed : _defaultSpeed * (_isCrouching ? _crouchSpeedMultiplier : 1f);

            Vector3 forward = _orientation.forward;
            forward.y = 0;
            Vector3 direction = input.y * forward + input.x * _orientation.right;

#if UNITY_6000_0_OR_NEWER
            Vector3 velocity = Rigidbody.linearVelocity;
#else
            Vector3 velocity = _rigidbody.velocity;
#endif

            velocity.y = 0;
            Vector3 force = _accelerationStrength * (targetSpeed * direction - velocity);

            Rigidbody.AddForce(force);
        }

        void HandleCrouchInput()
        {
            if (!_isCrouching && !CheckHeadRoom()) return;

            float newHeight = _isCrouching ? _crouchHeight : _defaultHeight;
            Collider.height = Mathf.Lerp(Collider.height, newHeight, 1f - Mathf.Exp(-Time.deltaTime * _crouchSmoothSpeed));
        }
        bool CheckHeadRoom() => Physics.SphereCastNonAlloc(Rigidbody.position + Collider.height * 0.55f * Vector3.up, Collider.radius * 0.99f, Vector3.up, _headCheckResults, _defaultHeight - Collider.height + 0.1f, _groundLayer, QueryTriggerInteraction.Ignore) == 0;

        public void SetMoveInput(Vector2 input) => _moveInput = input;
        public void SetSprint(bool isSprinting) => _isSprinting = isSprinting;
        public void SetCrouch(bool isSprinting) => _isCrouching = isSprinting;

        void OnValidate()
        {
            Collider.height = _defaultHeight;
            Vector3 newPos = Collider.transform.position;
            newPos.y = Collider.height * 0.5f + _springHeight;
            Collider.transform.position = newPos;
            _cameraTransform.localPosition = (_defaultHeight * 0.5f - 0.1f) * Vector3.up;
            _maxSlopeDot = 1f - (_maxSlopeAngle / 90f);
        }
    }
}
using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        Rigidbody _rigidbody;

        [SerializeField]
        CapsuleCollider _collider;

        [SerializeField]
        Transform _orientation;

        [SerializeField]
        Transform _cameraTransform;

        [Header("Acceleration")]
        [SerializeField]
        float _accelerationStrength = 1.5f;

        [SerializeField]
        float _accelerationExponent = 1.5f;

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
        float _springDamp = 1f;

        [SerializeField]
        float _springHeight = 0.25f;

        [SerializeField]
        float _springRaycastDistance = 0.5f;
        
        [SerializeField]
        LayerMask _groundLayer;
        
        [Header("Crouching")]
        [SerializeField]
        float _defaultHeight = 1.5f;

        [SerializeField]
        float _crouchHeight = 0.8f;

        Vector2 _moveInput;
        bool _isSprinting;
        bool _isCrouching;

        void FixedUpdate()
        {
            ApplySpringForce();
            ApplyMovementForce(_moveInput);
        }

        void ApplySpringForce()
        {
            Ray ray = new(_collider.transform.position - _collider.height * 0.49f * Vector3.up, Vector3.down);
            if (!Physics.SphereCast(ray, _collider.radius * 0.8f, out RaycastHit hit, _springRaycastDistance, _groundLayer))
            {
                return;
            }

            float force = (_springHeight - hit.distance) * _springForce - (_rigidbody.velocity.y * _springDamp);
            _rigidbody.AddForce(force * Time.fixedDeltaTime * Vector3.up);
        }
        void ApplyMovementForce(Vector2 input)
        {
            float targetSpeed = _isSprinting ? _sprintSpeed : _defaultSpeed * (_isCrouching ? _crouchSpeedMultiplier : 1f);

            Vector3 forward = Vector3.ProjectOnPlane(_orientation.forward, Vector3.up);
            Vector3 direction = input.y * forward + input.x * _orientation.right;

            Vector3 force = _accelerationStrength * (targetSpeed * direction - _rigidbody.velocity);

            _rigidbody.AddForce(force);
        }

        public void SetMoveInput(Vector2 input) => _moveInput = input;
        public void SetSprint(bool isSprinting) => _isSprinting = isSprinting;
        public void SetCrouch(bool isSprinting) => _isCrouching = isSprinting;

        void OnValidate()
        {
            _collider.height = _defaultHeight;
            Vector3 newPos = _collider.transform.position;
            newPos.y = _collider.height * 0.5f + _springHeight;
            _collider.transform.position = newPos;
            _cameraTransform.localPosition = (_defaultHeight * 0.5f - 0.1f) * Vector3.up;
        }
    }
}
using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        Transform _cameraTransform;

        [SerializeField]
        CapsuleCollider _playerCollider;

        [Header("Parameters")]
        [SerializeField]
        float _sensitivity = 1f;
        public float Sensitivity { get => _sensitivity; set => _sensitivity = value; }

        [SerializeField]
        float _smoothTime = 0.05f;
        public float SmoothTime { get => _smoothTime; set => _smoothTime = value; }

        [SerializeField]
        float _headRoom = 0.1f;

        [HideInInspector] public bool InvertMouseY { get; set; }

        Vector2 _lookInput;
        Vector3 _targetLookEulerAngles;

        float _rotX;
        float _rotY;
        float _rotXLerped;
        float _rotYLerped;

        float _mouseX;
        float _mouseY;
        float _rotXVelocity;
        float _rotYVelocity;

        void Update()
        {
            _rotXLerped = Mathf.SmoothDamp(_rotXLerped, _rotX, ref _rotXVelocity, _smoothTime);
            _rotYLerped = Mathf.SmoothDamp(_rotYLerped, _rotY, ref _rotYVelocity, _smoothTime);
            
            _cameraTransform.localRotation = Quaternion.Euler(_rotXLerped * Vector3.right);
            _playerCollider.transform.localRotation = Quaternion.Euler(_rotYLerped * Vector3.up);
            _cameraTransform.localPosition = (_playerCollider.height * 0.5f - _headRoom) * Vector3.up;
        }
        
        public void ApplyLookInput(Vector2 input)
        {
            _targetLookEulerAngles += _sensitivity * (input.x * Vector3.up - input.y * Vector3.right);
            _targetLookEulerAngles.x = Mathf.Clamp(_targetLookEulerAngles.x, -89f, 89f);

            _mouseX = input.x;
            _mouseY = input.y * (InvertMouseY ? -1 : 1);

            _rotX -= _mouseY * _sensitivity;
            _rotY += _mouseX * _sensitivity;
            _rotX = Mathf.Clamp(_rotX, -89f, 89f);
        }
    }
}
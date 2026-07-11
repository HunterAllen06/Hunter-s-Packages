using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        Transform _cameraTransform;

        [SerializeField]
        Transform _playerBodyTransform;

        [Header("Parameters")]
        [SerializeField]
        float _sensitivity = 1f;

        [SerializeField]
        float _smoothTime = 0.05f;

        Vector3 _targetLookEulerAngles;

        [HideInInspector] public bool InvertMouseY { get; set; }
        Vector2 _lookInput;

        [HideInInspector] public float RotX;
        [HideInInspector] public float RotY;
        [HideInInspector] public float RotXLerped;
        [HideInInspector] public float RotYLerped;

        float _mouseX;
        float _mouseY;
        float _rotXVelocity;
        float _rotYVelocity;

        void Update()
        {
            RotXLerped = Mathf.SmoothDamp(RotXLerped, RotX, ref _rotXVelocity, _smoothTime);
            RotYLerped = Mathf.SmoothDamp(RotYLerped, RotY, ref _rotYVelocity, _smoothTime);
            
            _cameraTransform.localRotation = Quaternion.Euler(RotXLerped * Vector3.right);
            _playerBodyTransform.localRotation = Quaternion.Euler(RotYLerped * Vector3.up);
        }
        
        public void ApplyLookInput(Vector2 input)
        {
            _targetLookEulerAngles += _sensitivity * (input.x * Vector3.up - input.y * Vector3.right);
            _targetLookEulerAngles.x = Mathf.Clamp(_targetLookEulerAngles.x, -89f, 89f);

            _mouseX = input.x;
            _mouseY = input.y * (InvertMouseY ? -1 : 1);

            RotX -= _mouseY * _sensitivity;
            RotY += _mouseX * _sensitivity;
            RotX = Mathf.Clamp(RotX, -89f, 89f);
        }
    }
}
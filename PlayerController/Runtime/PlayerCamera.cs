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
        float _smoothSpeed = 8f;

        Vector3 _targetLookEulerAngles;

        void Update()
        {
            _cameraTransform.localRotation = Quaternion.Slerp(_cameraTransform.localRotation, Quaternion.Euler(_targetLookEulerAngles.x * Vector3.right), 1f - Mathf.Exp(-Time.deltaTime * _smoothSpeed));
            _playerBodyTransform.localRotation = Quaternion.Slerp(_playerBodyTransform.localRotation, Quaternion.Euler(_targetLookEulerAngles.y * Vector3.up), 1f - Mathf.Exp(-Time.deltaTime * _smoothSpeed));
        }
        
        public void ApplyLookInput(Vector2 input)
        {
            _targetLookEulerAngles += _sensitivity * (input.x * Vector3.up - input.y * Vector3.right);
            _targetLookEulerAngles.x = Mathf.Clamp(_targetLookEulerAngles.x, -89f, 89f);
        }
    }
}
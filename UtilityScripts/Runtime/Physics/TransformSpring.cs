using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace HunterAllen.Utility
{
    public class TransformSpring : MonoBehaviour
    {
        #if ODIN_INSPECTOR
        [BoxGroup("Forces")]
        #endif
        public float SpringForce = 1f;

        #if ODIN_INSPECTOR
        [BoxGroup("Forces")]
        #endif
        public float SpringDamp = 1f;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Forces")]
        #endif
        public float TorqueSpring = 1f;

        #if ODIN_INSPECTOR
        [BoxGroup("Forces")]
        #endif
        public float TorqueDamp = 1f;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        #endif
        [SerializeField] 
        bool _applyPositionSpring;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        [ShowIf("_applyPositionSpring")]
        #endif
        public bool IsPositionLocal;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        #endif
        [SerializeField] 
        bool _applyRotationSpring;

        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        #endif
        [SerializeField]
        bool _applyScaleSpring;
        
        [Space]
        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        #endif
        [SerializeField] 
        bool _useFixedDeltaTime;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Settings")]
        #endif
        [SerializeField] 
        int presolveIterations;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Target")]
        #endif
        public Transform Target;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Target")]
        [HideIf("Target", null)]
        #endif
        public Vector3 TargetPosition;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Target")]
        [HideIf("Target", null)]
        #endif
        public Vector3 TargetRotation;
        
        #if ODIN_INSPECTOR
        [BoxGroup("Target")]
        [HideIf("Target", null)]
        #endif
        public Vector3 TargetScale;
    
        Vector3 _currentPosition;
        Vector3 _currentRotation;
        Vector3 _currentScale;
        
        Vector3 _positionVelocity;
        Vector3 _rotationVelocity;
        Vector3 _scaleVelocity;

        void Start()
        {
            Transform target = Target != null ? Target : transform;

            if (_applyPositionSpring)
            {
                _currentPosition = IsPositionLocal ? target.localPosition : target.position;

                for (int i = 0; i < presolveIterations; i++)
                {
                    ApplyPositionSpring(Time.fixedDeltaTime);
                }
            }
            if (_applyRotationSpring)
            {
                _currentRotation = target.eulerAngles;

                for (int i = 0; i < presolveIterations; i++)
                {
                    ApplyRotationSpring(Time.fixedDeltaTime);
                }
            }
            if (_applyScaleSpring)
            {
                _currentScale = target.localScale;

                for (int i = 0; i < presolveIterations; i++)
                {
                    ApplyScaleSpring(Time.fixedDeltaTime);
                }
            }
        }
        
        void Update()
        {
            if (_useFixedDeltaTime)
            {
                return;
            }

            if (_applyPositionSpring)
            {
                ApplyPositionSpring(Time.deltaTime);
            }
            if (_applyRotationSpring)
            {
                ApplyRotationSpring(Time.deltaTime);
            }
            if (_applyScaleSpring)
            {
                ApplyScaleSpring(Time.deltaTime);
            }
        }
    
        void FixedUpdate()
        {
            if (!_useFixedDeltaTime)
            {
                return;
            }

            if (_applyPositionSpring)
            {
                ApplyPositionSpring(Time.fixedDeltaTime);
            }
            if (_applyRotationSpring)
            {
                ApplyRotationSpring(Time.fixedDeltaTime);
            }
            if (_applyScaleSpring)
            {
                ApplyScaleSpring(Time.fixedDeltaTime);
            }
        }
    
        void ApplyPositionSpring(float deltaTime)
        {
            if (Target != null)
            {
                TargetPosition = IsPositionLocal ? Target.localPosition : Target.position;
            }
    
            _positionVelocity += Spring.GetSpringVector(_currentPosition, TargetPosition, _positionVelocity, SpringForce * 1200f, SpringDamp * 20f) * deltaTime;
            _positionVelocity = Vector3.ClampMagnitude(_positionVelocity, 1500f);
            _currentPosition += _positionVelocity * deltaTime;
    
            if (IsPositionLocal)
            {
                transform.localPosition = _currentPosition;
            }
            else
            {
                transform.position = _currentPosition;
            }
        }
        void ApplyRotationSpring(float deltaTime)
        {
            Vector3 targetAngle = Target != null ? (Target.eulerAngles - _currentRotation).ClampAsEuler() : TargetRotation;
            
            _rotationVelocity += Spring.GetSpringVector(targetAngle, _rotationVelocity, TorqueSpring * 100f, TorqueDamp * 10f) * deltaTime;
            _rotationVelocity = Vector3.ClampMagnitude(_rotationVelocity, 1500f);
            _currentRotation += _rotationVelocity * deltaTime;
            _currentRotation.ClampAsEuler();

            transform.eulerAngles = _currentRotation;
        }
        void ApplyScaleSpring(float deltaTime)
        {
            TargetScale = Target != null ? Target.localScale : TargetScale;
    
            _scaleVelocity += Spring.GetSpringVector(_currentScale, TargetScale, _scaleVelocity, SpringForce * 1200f, SpringDamp * 20f) * deltaTime;
            _scaleVelocity = Vector3.ClampMagnitude(_scaleVelocity, 1500f);
            _currentScale += _scaleVelocity * deltaTime;
    
            transform.localScale = _currentScale;
        }
    
        public void ApplyScaleImpulse(float impulse)
        {
            _scaleVelocity += impulse * Vector3.one;
        }
        public void SetUniformScaleTarget(float scale)
        {
            TargetScale = scale * Vector3.one;
        }
    }
    [Serializable]
    public class TransformSpringData
    {
        public EAxis Axis;
        public bool IsLocal;
    }
}
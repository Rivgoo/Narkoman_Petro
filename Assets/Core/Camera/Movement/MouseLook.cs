using System;
using Core.InputSystem; 
using UnityEngine;
using Core.Camera.Movement.Data;

namespace Core.Camera.Movement
{
	public class MouseLook : MonoBehaviour
    {
        [SerializeField]
        private MouseInput _input;

        [Space]
		[SerializeField] private CameraSettings _cameraMove;
		[Space]
		[SerializeField] private Transform _player;

        private Quaternion _characterTargetRot;
        private Quaternion _cameraTargetRot; 
        
        private static float _targetMaxX;
        private static float _targetMinX;
		
        public static void AddOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX -= values.Down;
        	_targetMinX += values.Top;
        }
        
        public static void SubtractOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX += values.Down;
        	_targetMinX -= values.Top;
        }
        
        private void UpdateMaxValueRotation()
        {
        	_cameraMove.Move.CurrentAngle.Down = Mathf.Lerp(_cameraMove.Move.CurrentAngle.Down, _targetMaxX, _cameraMove.Move.SpeedTransition * Time.deltaTime);
        	_cameraMove.Move.CurrentAngle.Top = Mathf.Lerp(_cameraMove.Move.CurrentAngle.Top, _targetMinX, _cameraMove.Move.SpeedTransition * Time.deltaTime);
        }

        private void LookRotation()	
        {
        	float yRot = _input.GetXAxes() * _cameraMove.Move.XSensitivity;
        	float xRot = _input.GetYAxes() * _cameraMove.Move.YSensitivity;

            _characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            _cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            _cameraTargetRot = ClampRotationAroundXAxis (_cameraTargetRot);

            _player.localRotation = Quaternion.Slerp (_player.localRotation, _characterTargetRot, _cameraMove.Move.SlowingRotation * Time.deltaTime);
            _cameraMove.Camera.localRotation = Quaternion.Slerp ( _cameraMove.Camera.localRotation, _cameraTargetRot, _cameraMove.Move.SlowingRotation * Time.deltaTime);
        }
        
        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
			
            angleX = Mathf.Clamp(angleX, _targetMinX, _targetMaxX);
            angleX = Mathf.Clamp (angleX, _cameraMove.Move.DefoultAngle.Top, _cameraMove.Move.DefoultAngle.Down);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
        
        private void Update()
        {
        	LookRotation();
        	UpdateMaxValueRotation();
        }
        
        private void Start()
        {
        	Setup();
        }
        
        private void Setup()
        {
        	_targetMaxX = _cameraMove.Move.DefoultAngle.Down;
            _targetMinX = _cameraMove.Move.DefoultAngle.Top;
            
            _characterTargetRot = _player.localRotation;
            _cameraTargetRot = _cameraMove.Camera.localRotation;
        }
    }
}

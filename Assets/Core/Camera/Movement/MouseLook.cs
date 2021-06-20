using System;
using Input = PlayerInput.MouseInput; 
using UnityEngine;
using Core.Camera.Movement.Data;

namespace Core.Camera.Movement
{
	public class MouseLook : MonoBehaviour
    {
		[SerializeField] private CameraSettings _cameraMove;
		[Space]
		[SerializeField] private Transform _player;

        private Quaternion _characterTargetRot;
        private Quaternion _cameraTargetRot; 
        
        private static float _targetMaxX;
        private static float _targetMinX;
		
        public static void AddOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX -= values.Maximum;
        	_targetMinX += values.Minimum;
        }
        
        public static void SubtractOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX += values.Maximum;
        	_targetMinX -= values.Minimum;
        }
        
        private void UpdateMaxValueRotation()
        {
        	_cameraMove.Move.CurrentAngle.Maximum = Mathf.Lerp(_cameraMove.Move.CurrentAngle.Maximum, _targetMaxX, _cameraMove.Move.SpeedTransition * Time.deltaTime);
        	_cameraMove.Move.CurrentAngle.Minimum = Mathf.Lerp(_cameraMove.Move.CurrentAngle.Minimum, _targetMinX, _cameraMove.Move.SpeedTransition * Time.deltaTime);
        }

        private void LookRotation()	
        {
        	float yRot = Input.GetXAxes() * _cameraMove.Move.XSensitivity;
        	float xRot = Input.GetYAxes() * _cameraMove.Move.YSensitivity;

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
            angleX = Mathf.Clamp (angleX, _cameraMove.Move.DefoultAngle.Minimum, _cameraMove.Move.DefoultAngle.Maximum);

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
        	_targetMaxX = _cameraMove.Move.DefoultAngle.Maximum;
            _targetMinX = _cameraMove.Move.DefoultAngle.Minimum;
            
            _characterTargetRot = _player.localRotation;
            _cameraTargetRot = _cameraMove.Camera.localRotation;
        }
    }
}

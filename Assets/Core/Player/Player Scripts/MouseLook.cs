using System;
using UnityEngine;
using PlayerData;

namespace Player
{
	public class MouseLook
    {
    	private MovementCameraData _cameraMove;

        private Quaternion _characterTargetRot;
        private Quaternion _cameraTargetRot; 
        
        private float _targetMaxX;
        private float _targetMinX;
		
        public void AddOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX -= values.Maximum;
        	_targetMinX += values.Minimum;
        }
        
        public void SubtractOffsetValueRatation(Vector2Angle values)
        {
        	_targetMaxX += values.Maximum;
        	_targetMinX -= values.Minimum;
        }
        
        public void UpdateMaxValueRotation()
        {
        	_cameraMove.CameraMove.CurrentAngle.Maximum = Mathf.Lerp(_cameraMove.CameraMove.CurrentAngle.Maximum, _targetMaxX, _cameraMove.CameraMove.SpeedTransition * Time.deltaTime);
        	_cameraMove.CameraMove.CurrentAngle.Minimum = Mathf.Lerp(_cameraMove.CameraMove.CurrentAngle.Minimum, _targetMinX, _cameraMove.CameraMove.SpeedTransition * Time.deltaTime);
        }
        
        public void Init(Transform character, MovementCameraData cameraData)
        {
        	_cameraMove = cameraData;
        	
        	_targetMaxX = _cameraMove.CameraMove.DefoultAngle.Maximum;
            _targetMinX = _cameraMove.CameraMove.DefoultAngle.Minimum;
            
            _characterTargetRot = character.localRotation;
            _cameraTargetRot = cameraData.Camera.localRotation;
        }

        public void LookRotation(Transform character)
        {
            float yRot = Input.GetAxis("Mouse X") * _cameraMove.CameraMove.XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * _cameraMove.CameraMove.YSensitivity;

            _characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            _cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            _cameraTargetRot = ClampRotationAroundXAxis (_cameraTargetRot);

            character.localRotation = Quaternion.Slerp (character.localRotation, _characterTargetRot, _cameraMove.CameraMove.TimeInterpolateRotation * Time.deltaTime);
            _cameraMove.Camera.localRotation = Quaternion.Slerp ( _cameraMove.Camera.localRotation, _cameraTargetRot, _cameraMove.CameraMove.TimeInterpolateRotation * Time.deltaTime);
        }
        
        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, _cameraMove.CameraMove.DefoultAngle.Minimum, _cameraMove.CameraMove.DefoultAngle.Maximum);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}

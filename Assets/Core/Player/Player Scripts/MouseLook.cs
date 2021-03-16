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
		
        public void AddOffsetValueRatation(Vector2 values)
        {
        	_targetMaxX -= values.x;
        	_targetMinX += values.y;
        }
        
        public void SubtractOffsetValueRatation(Vector2 values)
        {
        	_targetMaxX += values.x;
        	_targetMinX -= values.y;
        }
        
        public void UpdateMaxValueRotation()
        {
        	_cameraMove.CameraMove.MaximumX = Mathf.Lerp(_cameraMove.CameraMove.MaximumX, _targetMaxX, _cameraMove.CameraMove.SpeedTransition * Time.deltaTime);
        	_cameraMove.CameraMove.MinimumX = Mathf.Lerp(_cameraMove.CameraMove.MinimumX, _targetMinX, _cameraMove.CameraMove.SpeedTransition * Time.deltaTime);
        }
        
        public void Init(Transform character, MovementCameraData cameraData)
        {
        	_cameraMove = cameraData;
        	
        	_targetMaxX = _cameraMove.CameraMove.MaximumX;
            _targetMinX = _cameraMove.CameraMove.MinimumX;
            
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

            character.localRotation = Quaternion.Slerp (character.localRotation, _characterTargetRot, _cameraMove.CameraMove.SmoothTime * Time.deltaTime);
            _cameraMove.Camera.localRotation = Quaternion.Slerp ( _cameraMove.Camera.localRotation, _cameraTargetRot, _cameraMove.CameraMove.SmoothTime * Time.deltaTime);
        }
        
        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, _cameraMove.CameraMove.MinimumX, _cameraMove.CameraMove.MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}

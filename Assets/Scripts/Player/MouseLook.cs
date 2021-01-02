using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    [SerializeField]
    internal class MouseLook
    {
    	[SerializeField] private MouseData _mouse;

        private Quaternion _characterTargetRot;
        private Quaternion _cameraTargetRot; 
		
        internal void Init(Transform character, Transform camera)
        {
            _characterTargetRot = character.localRotation;
            _cameraTargetRot = camera.localRotation;
        }

        internal void LookRotation(Transform character, Transform camera)
        {
            float yRot = Input.GetAxis("Mouse X") * _mouse.XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * _mouse.YSensitivity;

            _characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            _cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            _cameraTargetRot = ClampRotationAroundXAxis (_cameraTargetRot);

            character.localRotation = Quaternion.Slerp (character.localRotation, _characterTargetRot, _mouse.smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp (camera.localRotation, _cameraTargetRot, _mouse.smoothTime * Time.deltaTime);
        }

        internal static void CursoreLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
            	ShowCursor();
            }
            else if(Input.GetMouseButtonUp(0))
            {
            	HideCursor();
            }
        }
        
        internal static void HideCursor()
        {
        	Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
      	internal static void ShowCursor()
        {
        	 Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }
        
        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, _mouse.MinimumX, _mouse.MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}

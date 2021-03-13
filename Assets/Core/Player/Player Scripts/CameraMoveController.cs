using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey; 
using Random = UnityEngine.Random;
using PlayerData;

namespace Player
{
	public class CameraMoveController : MonoBehaviour
	{
		private MouseLook _mouseLook;
		
		private MovementPlayerData _movementPlayer;
    	private MovementCameraData _movementCamera;
    	
    	public void Init(MovementPlayerData movementPlayer, MovementCameraData movementCamera, MouseLook mouseLook)
        {
    		_mouseLook = mouseLook;
    		
        	_movementPlayer = movementPlayer;
        	_movementCamera = movementCamera;
        	
        	_movementCamera.OriginalCameraPosition = _movementCamera.Camera.localPosition;
            _movementCamera.Shake.HeadBob.Setup(_movementCamera.Camera, _movementPlayer.Step.StepInterval);
            
			MouseLook.HideCursor(); // TODO
        }
		
    	private void UpdateCameraPosition(float speed)
        {
            var tempCameraPosition = Vector3.zero;
			
            if (!_movementPlayer.State.UpCharacter)
            {
	            if (_movementPlayer.CharacterController.isGrounded)
	            {    
	            	if (!_movementPlayer.State.IsMovePlayer) 
	            	{
	            		_movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, _movementCamera.Shake.HeadBob.DoHeadBob(1), Time.fixedDeltaTime * _movementCamera.Shake.SpeedCameraLerp);
					
	            	}
	            	else
	            	{
						_movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, _movementCamera.Shake.HeadBob.DoHeadBob(_movementPlayer.CharacterController.velocity.magnitude + speed), Time.fixedDeltaTime * _movementCamera.Shake.SpeedCameraLerp);
	            	}
	            	
	                tempCameraPosition =  _movementCamera.Camera.localPosition;
	                tempCameraPosition.y = _movementCamera.Camera.localPosition.y - _movementCamera.JumpShake.JumpBob.Offset();
	            }
	            else
	            {
	            	tempCameraPosition = Vector3.Lerp(tempCameraPosition, _movementCamera.Camera.localPosition, Time.fixedDeltaTime * _movementCamera.Shake.SpeedCameraLerp);
	                tempCameraPosition.y = _movementCamera.OriginalCameraPosition.y - _movementCamera.JumpShake.JumpBob.Offset();
	            } 
            }

            tempCameraPosition += _movementCamera.Shake.CameraVector;
            tempCameraPosition = new Vector3(Mathf.Clamp(tempCameraPosition.x, -_movementCamera.Shake.MaxXShakeCamera, _movementCamera.Shake.MaxXShakeCamera), Mathf.Clamp(tempCameraPosition.y, _movementCamera.Shake.MinHeightCamera, _movementCamera.Shake.MaxHeightCamera), Mathf.Clamp(tempCameraPosition.z, -_movementCamera.Shake.MaxZShakeCamera, _movementCamera.Shake.MaxZShakeCamera));
            
            _movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, tempCameraPosition, Time.fixedDeltaTime * _movementCamera.Shake.SpeedCameraLerp);
        }
    	
		private void RotateView()
        {
            _mouseLook.LookRotation(transform, _movementCamera.Camera.transform);
        }
		
		private void HeadBob()
        {
        	if (!_movementPlayer.State.IsMovePlayer)
        	{
        		_movementCamera.Shake.HeadBob.SetTypeBob(TypeBob.Stay);
        	} 
        	else if (_movementPlayer.State.IsWalking)
        	{
        		_movementCamera.Shake.HeadBob.SetTypeBob(TypeBob.Walk);
        	} 
        	else if (_movementPlayer.State.IsRun) 
        	{
        		_movementCamera.Shake.HeadBob.SetTypeBob(TypeBob.Run);
        	} 
        	else if (_movementPlayer.State.IsCrouch)
        	{
        		_movementCamera.Shake.HeadBob.SetTypeBob(TypeBob.Crouch);
        	} 
        }
		
		private void Update()
		{
			HeadBob();
			RotateView();
			
			 MouseLook.CursoreLockUpdate(); //TODO: When will Add Menu UI then delete this method
		}
		
		private void FixedUpdate()
		{
			UpdateCameraPosition(_movementPlayer.SpeedsSettings.CurrentSpeed); 
		}
	}
}
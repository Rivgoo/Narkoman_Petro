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
    	
    	/// <summary>
    	/// Init Script
    	/// </summary>
    	/// <param name="movementPlayer">MovementPlayerData</param>
    	/// <param name="movementCamera">MovementCameraData</param>
    	/// <param name="mouseLook">MouseLook Scripts</param>
    	public void Init(MovementPlayerData movementPlayer, MovementCameraData movementCamera, MouseLook mouseLook)
        {
    		_mouseLook = mouseLook;
    		
        	_movementPlayer = movementPlayer;
        	_movementCamera = movementCamera;
        	
        	_movementCamera.OriginalCameraPosition = _movementCamera.Camera.localPosition;
            _movementCamera.Shake.HeadBob.Setup(_movementCamera.Camera, _movementPlayer.Step.StepInterval);  
            
			HideCursor(); // TODO
        }
		
    	public static void HideCursor()
        {
        	Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
      	public static void ShowCursor()
        {
        	 Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }
      	
    	private static void CursoreLockUpdate()
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
    	
    	private void UpdateCameraPosition()
        {
            var tempCameraPosition = Vector3.zero;
			var speedLerp = Time.fixedDeltaTime * _movementCamera.Shake.SpeedCameraLerp;
			
            if (!_movementPlayer.State.UpCharacter)
            {
	            if (_movementPlayer.CharacterController.isGrounded)
	            {   
	            	if (!_movementPlayer.State.IsMovePlayer) 
	            	{
	            		//Set the Head Bob for the camera if player don't move
	            		_movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, _movementCamera.Shake.HeadBob.PlayHeadBob(1), speedLerp);
					
	            	}
	            	else
	            	{
	            		//Set the Head Bob for the camera if player move
						_movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, _movementCamera.Shake.HeadBob.PlayHeadBob(_movementPlayer.CharacterController.velocity.magnitude + _movementPlayer.Speeds.Current), speedLerp);
	            	}
	            	
	                tempCameraPosition =  _movementCamera.Camera.localPosition;
	                // Set Jump Offset
	                tempCameraPosition.y = _movementCamera.Camera.localPosition.y - _movementCamera.JumpShake.JumpBob.Offset();
	            }
	            else //If Player Jumping
	            {
	            	tempCameraPosition = Vector3.Lerp(tempCameraPosition, _movementCamera.Camera.localPosition, speedLerp);
	            	 // Set Jump Offset
	                tempCameraPosition.y = _movementCamera.OriginalCameraPosition.y - _movementCamera.JumpShake.JumpBob.Offset();
	            } 
            }
			
            // Add a vector to create effects
            tempCameraPosition += _movementCamera.Shake.CameraVector; 
            
            // Clamp Max Value Camera. BUG prevention!
            var x = Mathf.Clamp(tempCameraPosition.x, -_movementCamera.Shake.MaxXShakeCamera, _movementCamera.Shake.MaxXShakeCamera);
            var y = Mathf.Clamp(tempCameraPosition.y, _movementCamera.Shake.MinHeightCamera, _movementCamera.Shake.MaxHeightCamera);
            var z = Mathf.Clamp(tempCameraPosition.z, -_movementCamera.Shake.MaxZShakeCamera, _movementCamera.Shake.MaxZShakeCamera);
   
            tempCameraPosition = new Vector3(x, y, z);
            
            // Final moving the camera
            _movementCamera.Camera.localPosition = Vector3.Lerp(_movementCamera.Camera.localPosition, tempCameraPosition, speedLerp);
        }
    	
		private void RotateView()
        {
            _mouseLook.LookRotation(transform);
        }
		
		private void ChangeTypeHeadBob()
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
			CursoreLockUpdate(); //TODO: When will Add Menu UI then delete this 
			ChangeTypeHeadBob();
			RotateView();
			
			_mouseLook.UpdateMaxValueRotation(); 
		}
		
		private void FixedUpdate()
		{
			UpdateCameraPosition(); 
		}
	}
}
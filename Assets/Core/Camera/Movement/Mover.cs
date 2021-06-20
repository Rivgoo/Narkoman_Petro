using UnityEngine;
using Keys = PlayerInput.KeysInput.CheckMovementKey; 
using Random = UnityEngine.Random;
using Core.Player.Movement;
using Core.Player.Movement.Data;
using Core.Camera.Effects;
using Core.Camera.Movement.Data;

namespace Core.Camera.Movement
{
	public class Mover : MonoBehaviour
	{
		[SerializeField] private PlayerMovementStates _states;
        [SerializeField] private PlayerMovement _playerMovement;
    	[SerializeField] private CameraSettings _cameraMovement;
    	
    	private Vector3 _targetCameraPosition;
    	
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
            _targetCameraPosition = Vector3.zero;
            
			var shakeSpeed = Time.fixedDeltaTime * _cameraMovement.Shake.Speed;
			
            if (!_states.States.Risesing)
            {
                if (_playerMovement.Movement.CharacterController.isGrounded)
	            {   
	            	PlayHeadBob(shakeSpeed);
	            	
	                _targetCameraPosition =  _cameraMovement.Camera.localPosition;
	                // Set Jump Offset
	                _targetCameraPosition.y = _cameraMovement.Camera.localPosition.y - _cameraMovement.JumpShake.Offset;
	            }
	            else //If Player Jumping
	            {
	            	_targetCameraPosition = Vector3.Lerp(_targetCameraPosition, _cameraMovement.Camera.localPosition, shakeSpeed);
	            	 // Set Jump Offset
	                _targetCameraPosition.y = _cameraMovement.OriginalCameraPosition.y - _cameraMovement.JumpShake.Offset;
	            }
            }


            // Add crouch vector
            _targetCameraPosition += _cameraMovement.Shake.CrouchVector;

            //Clamp offset value
            _targetCameraPosition = ClampTargetCameraPosition(_targetCameraPosition);

            // Final moving the camera
            _cameraMovement.Camera.localPosition = Vector3.Lerp(_cameraMovement.Camera.localPosition, _targetCameraPosition, shakeSpeed);
        }
			
    	private void PlayHeadBob(float lerpSpeed)
    	{
    		float speedHeadBob = 1;
	            	
	        if (_states.States.Moving) 
	        {
                speedHeadBob = _playerMovement.Movement.CharacterController.velocity.magnitude + _playerMovement.SpeedsValue.Current;
	        }
	            	
	        _cameraMovement.Camera.localPosition = Vector3.Lerp(_cameraMovement.Camera.localPosition, _cameraMovement.Shake.HeadBob.PlayHeadBob(speedHeadBob), lerpSpeed);
    	}
    	
    	private Vector3 ClampTargetCameraPosition(Vector3 targetPosition)
    	{
    		// Clamp Max offset Value Camera. BUG prevention!
            var x = Mathf.Clamp(targetPosition.x, -_cameraMovement.Shake.MaxXShakeCamera, _cameraMovement.Shake.MaxXShakeCamera);
            var y = Mathf.Clamp(targetPosition.y, _cameraMovement.Shake.MinHeightCamera, _cameraMovement.Shake.MaxHeightCamera);
            var z = Mathf.Clamp(targetPosition.z, -_cameraMovement.Shake.MaxZShakeCamera, _cameraMovement.Shake.MaxZShakeCamera);

            return new Vector3(x, y, z);
    	}
    	
    	private void ChangeTypeHeadBob()
        {
        	if (!_states.States.Moving)
        	{
        		_cameraMovement.Shake.HeadBob.SetTypeBob(TypeBob.Stay);
        	} 
        	else if (_states.States.Crouching)
        	{
        		_cameraMovement.Shake.HeadBob.SetTypeBob(TypeBob.Crouch);
        	} 
        	else if (_states.States.Walking)
        	{
        		_cameraMovement.Shake.HeadBob.SetTypeBob(TypeBob.Walk);
        	} 
        	else if (_states.States.Running) 
        	{
        		_cameraMovement.Shake.HeadBob.SetTypeBob(TypeBob.Run);
        	} 
        }
		
		private void Update()
		{
			CursoreLockUpdate(); //TODO: When will Add Menu UI then delete this 
			ChangeTypeHeadBob();
		}
		
		private void FixedUpdate()
		{
			UpdateCameraPosition(); 
		}
		
		private void Setup()
        {
        	_cameraMovement.OriginalCameraPosition = _cameraMovement.Camera.localPosition;
            _cameraMovement.Shake.HeadBob.Setup(_cameraMovement.Camera, _playerMovement.Step.StepInterval, _cameraMovement.Bobs);
        }
		
    	private void Start()
    	{
    		HideCursor(); // TODO
    		Setup();
    	}
	}
}
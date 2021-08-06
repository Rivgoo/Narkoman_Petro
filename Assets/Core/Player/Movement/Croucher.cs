using UnityEngine;
using System.Collections;
using Core.InputSystem; 
using Random = UnityEngine.Random;
using Core.Camera.Movement;
using Core.Player.Movement.Data;
using Core.Camera.Movement.Data;

namespace Core.Player.Movement
{
	public class Croucher : MonoBehaviour
	{
        [SerializeField]
        private KeyboardInput _inputKeys;

        [Space]
		[SerializeField] 
        private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerMovementStates _movementStates;

    	[SerializeField] 
        private CameraSettings _cameraMovement;

        private readonly float _offsetToSetNormalHeight = 0.05f;

		private void Crouch()
		{
            if (!_movementStates.States.Risesing && !_movementStates.States.Crouching)
            {
                _cameraMovement.Shake.CrouchVector = Vector3.zero;
            }

            if (_movementStates.States.Crouching)
	        {
                _playerMovement.Movement.CharacterController.height = Mathf.Lerp(_playerMovement.Movement.CharacterController.height, _playerMovement.Crouch.HeightWhenCrouching, Time.fixedDeltaTime * _playerMovement.Crouch.SpeedDown);
				CrouchCameraDown();
			}
			else
			{
                if (_playerMovement.Movement.CharacterController.height > _playerMovement.Crouch.CharacterNormalHeight - _offsetToSetNormalHeight)
				{
                    _playerMovement.Movement.CharacterController.height = _playerMovement.Crouch.CharacterNormalHeight;
                    _movementStates.States.Risesing = false;
					return;
				}
				
				CrouchCameraUp();
				transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + (_playerMovement.Crouch.CharacterNormalHeight - _playerMovement.Movement.CharacterController.height), transform.position.z),Time.fixedDeltaTime * _playerMovement.Crouch.SpeedUp);
				
				_playerMovement.Movement.CharacterController.height = Mathf.Lerp(_playerMovement.Movement.CharacterController.height, _playerMovement.Crouch.CharacterNormalHeight, Time.fixedDeltaTime * _playerMovement.Crouch.SpeedUp);
                _movementStates.States.Risesing = true;

			}
		}                               
		
		private void CrouchCameraDown()
		{
			var target = new Vector3(_cameraMovement.Shake.CrouchVector.x, _playerMovement.Crouch.CameraLoweringDistance, _cameraMovement.Shake.CrouchVector.z);
		
			_cameraMovement.Shake.CrouchVector = Vector3.Lerp(_cameraMovement.Shake.CrouchVector, target, Time.fixedDeltaTime *  _playerMovement.Crouch.SpeedDown);
		}
		
		private void CrouchCameraUp()
		{
			var target = new Vector3(_cameraMovement.Shake.CrouchVector.x, _cameraMovement.OriginalCameraPosition.y, _cameraMovement.Shake.CrouchVector.z);
		
			_cameraMovement.Shake.CrouchVector = Vector3.Lerp(_cameraMovement.Shake.CrouchVector, target, Time.fixedDeltaTime *  _playerMovement.Crouch.SpeedUp);
		}
		
        private void CheckCrouch()
	        {
                if (_inputKeys.CrouchDown() && !_movementStates.States.Jumping && !_movementStates.BlockingTypeMovements.Crouch)
	        	{
                    _movementStates.States.Crouching = true;
	        		_playerMovement.Crouch.IsCheckCrouchRaycast = false; 
	        		return;
	        	} 
	        	else if (_inputKeys.CrouchUp())
	        	{
	        		_playerMovement.Crouch.IsCheckCrouchRaycast = true;
	        	}
	        	
	        	if (_playerMovement.Crouch.IsCheckCrouchRaycast) 
	        	{
	        		if (!Jumper.RaycastUp(transform.position, _playerMovement.Crouch.DistanceCheckRaycasting)) 
	        		{
                        _movementStates.States.Crouching = false;
	        			_playerMovement.Crouch.IsCheckCrouchRaycast = false;
	        		}
	        	}
	        }
        
        private void Update()
        {   
        	CheckCrouch(); 
        } 
        
        private void FixedUpdate()
        {  
            Crouch();
        }
	}
}

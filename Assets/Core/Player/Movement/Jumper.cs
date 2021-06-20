using UnityEngine;
using Keys = PlayerInput.KeysInput.CheckMovementKey; 
using Random = UnityEngine.Random;
using Core.Player.Characteristics;
using Core.Camera.Movement;
using Core.Player.Movement.Data;
using Core.Camera.Movement.Data;

namespace Core.Player.Movement
{
	public class Jumper : MonoBehaviour
	{
		[SerializeField] 
        private PlayerMovement _movementPlayer;

        [SerializeField]
        private PlayerMovementStates _state;

    	[SerializeField] 
        private CameraSettings _movementCamera;

        [SerializeField]
        private PlayerPhysic _playerPhysic;

    	[Space]
       	[SerializeField] 
        private PlayerSound _sound;

        [SerializeField]
        private EndurancePlayer _endurance;  

        private bool _isJump;
        
        /// <summary>
        /// Do RayCast Up.
        /// </summary>
        /// <param name="origin">Start position ray.</param>
        /// <param name="maxDistancy">Maximum distancy raycast</param>
        /// <returns></returns>
        public static bool RaycastUp(Vector3 origin, float maxDistancy)
        {
        	return Physics.Raycast(origin, Vector3.up, maxDistancy);
        }
        
        private void Update()
        {   
        	CheckJump();
            EndJumping(); 
        } 
        
        private void FixedUpdate()
        {  
            Jump();
        }
		
        private void EndJumping()
        { 
        	//previously grounded + is grounded + not player rises + not crouching
            if (!_state.States.PreviouslyGrounded && _movementPlayer.Movement.CharacterController.isGrounded && !_state.States.Risesing && !_state.States.Crouching)
            {
                StartCoroutine(_movementCamera.JumpShake.PlayBobCycle());
                
                PlayLandingSound();
                UpdateStep();
                
                _movementPlayer.Movement.Direction.y = 0f;

                _state.States.Jumping = false;
            }
        }
		
        private void PlayLandingSound()
        {
        	_sound.PlayLandingSound();
        } 
		
        private void UpdateStep()
        {
        	_movementPlayer.Step.NextStep = _movementPlayer.Step.StepCycle + .5f;
        }
        
        private void Jump()
        {
        	if (_movementPlayer.Movement.CharacterController.isGrounded && _movementPlayer.Movement.Collision != CollisionFlags.Above)
            {
                _movementPlayer.Movement.Direction.y = -_playerPhysic.Gravity.StickToGroundForce;

                if (_isJump)
                {
                    _movementPlayer.Movement.Direction.y = _movementPlayer.SpeedsValue.Jump;
                    
                    _sound.PlayJumpSound();

                    _isJump = false;
                    _state.States.Jumping = true;
                    
                    _endurance.SubtractEnduranceOfJump();
                }
            }
            else
            {
            	if (_movementPlayer.Movement.Collision == CollisionFlags.Above)
            	{
            		_movementPlayer.Movement.Direction -= new Vector3(0, 0.8f, 0);
            	}
            	else
            	{
                    _movementPlayer.Movement.Direction += Physics.gravity * _playerPhysic.Gravity.GravityForce * Time.fixedDeltaTime;
            
            	}
            }
        }
        
        private void CheckJump()
        {
        	//the top is empty + not block all move + not block jump
        	if (!RaycastUp(transform.position, 1.5f) && !_state.BlockingTypeMovements.Jump)
        	{	
        		// not jumping + not crouching + not player rises
                if (!_isJump && !_state.States.Crouching && !_state.States.Risesing)
	            {
                    _isJump = _endurance.CheckCanJump() && _movementPlayer.Movement.CharacterController.isGrounded;
	            }
        	}
        }
	
	}
}

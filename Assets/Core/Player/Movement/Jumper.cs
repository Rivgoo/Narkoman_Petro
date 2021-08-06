using UnityEngine;
using Core.Player.Characteristics;
using Core.Camera.Movement;
using Core.Player.Movement.Data;
using Core.Camera.Movement.Data;
using Core.InputSystem;
using Random = UnityEngine.Random;

namespace Core.Player.Movement
{
	public class Jumper : MonoBehaviour
    {
        [SerializeField]
        private KeyboardInput _inputKeys;

        [Space]
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
        private Sound.Player.MovementSoundsPlayer _soundsPlayer;

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
                
                UpdateStep();
                
                _movementPlayer.Movement.Direction.y = 0f;

                _state.States.Jumping = false;

                _soundsPlayer.PlayLanding();
            }
        }

        private void UpdateStep()
        {
        	_movementPlayer.Step.NextStep = _movementPlayer.Step.StepCycle + .5f;
        }

        private void DoJump()
        {
            _movementPlayer.Movement.Direction.y = -_playerPhysic.Gravity.StickToGroundForce;

            if(_isJump)
            {
                _movementPlayer.Movement.Direction.y = _movementPlayer.SpeedsValue.Jump;

                _isJump = false;
                _state.States.Jumping = true;

                _endurance.SubtractEnduranceOfJump();

                _soundsPlayer.PlayJump();
            }
        }

        private void DoLanding()
        {
            if(_movementPlayer.Movement.Collision == CollisionFlags.Above)
            {
                _movementPlayer.Movement.Direction -= new Vector3(0, 0.8f, 0);
            }
            else
            {
                _movementPlayer.Movement.Direction += Physics.gravity * _playerPhysic.Gravity.GravityForce * Time.fixedDeltaTime;

            }
        }

        private void Jump()
        {
        	if (_movementPlayer.Movement.CharacterController.isGrounded && _movementPlayer.Movement.Collision != CollisionFlags.Above)
            {
                DoJump();
            }
            else
            {
                DoLanding();
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
                    _isJump = _inputKeys.JumpDown() && _endurance.CheckCanJump() && _movementPlayer.Movement.CharacterController.isGrounded;
	            }
        	}
        }
	}
}

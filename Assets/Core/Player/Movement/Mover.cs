using UnityEngine;
using Core.InputSystem; 
using Random = UnityEngine.Random;
using Core.Player.Characteristics;
using Core.Player.Movement.Data;

namespace Core.Player.Movement
{
	public class Mover : MonoBehaviour
	{
        [SerializeField]
        private KeyboardInput _inputKeys;

        [Space]
		[SerializeField] 
        private PlayerMovement _movementPlayer;

        [SerializeField]
        private PlayerMovementStates _state;

    	[Space]
        [SerializeField] 
        private EndurancePlayer _endurance;
          
        private void Update()       
        {   
        	CheckMovePlayer(); 
        	CheckRun();
        	CheckWalk();
        	
            ChooseCurrentTypeSpeed();
            
            NullDirectionY();
            PreviouslyGrounded();
        } 
         
        private void FixedUpdate()
        {  
            GetPlayerInput();
            
            CalculateMoveDirection();
            Move();
        }
        
        private void NullDirectionY()
        {
            if (!_movementPlayer.Movement.CharacterController.isGrounded && !_state.States.Jumping && _state.States.PreviouslyGrounded)
            {
                 _movementPlayer.Movement.Direction.y = 0f;
            }
        }
        
        private void PreviouslyGrounded()
        {
            _state.States.PreviouslyGrounded = _movementPlayer.Movement.CharacterController.isGrounded;
        }
        
        private void CheckRun()
        {
            _state.States.Running = _endurance.CheckRunKeyIsStay();
        	
        	//Runing + Not Crouch + Not Block Move + Not Block Run
            _state.States.Running = _state.States.Running && !_state.States.Crouching && !_state.BlockingTypeMovements.Run;
        }
        
        private void CheckWalk()
        {
            _state.States.Walking = !_inputKeys.Run() && !_endurance.CheckCanRun() && !_state.States.Crouching && !_state.BlockingTypeMovements.Walk;
        }
        
        private void ChooseCurrentTypeSpeed()
        {   
        	float inputSpeed = 0;

            if (_state.States.Crouching) 
        	{
        		inputSpeed = _movementPlayer.SpeedsValue.Crouch;
                _state.States.CurrentTypeMovement = TypeMovement.Crouch;
        	}
        	else if(_movementPlayer.Movement.UserInput == Vector2.zero)
        	{
        		inputSpeed = 0;
                _state.States.CurrentTypeMovement = TypeMovement.None;
        	}
            else if (_state.States.Walking)
        	{
        		inputSpeed = _movementPlayer.SpeedsValue.Walk;
                _state.States.CurrentTypeMovement = TypeMovement.Walk;
        	}
        	else
        	{
        		inputSpeed = _movementPlayer.SpeedsValue.Run;
                _state.States.CurrentTypeMovement = TypeMovement.Run;
        	}
        	
        	_movementPlayer.SpeedsValue.Current = Mathf.MoveTowards(_movementPlayer.SpeedsValue.Current, inputSpeed, Time.fixedDeltaTime * _movementPlayer.SpeedsSettings.SpeedTransitionBetweenSpeeds);
        }
        
        private void CheckMovePlayer()
        {
            _state.States.Moving = _movementPlayer.Movement.CharacterController.velocity.magnitude > 0; 
        } 
        
        private void CalculateMoveDirection()
        {
            RaycastHit hitInfo;

            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * _movementPlayer.Movement.UserInput.y + transform.right * _movementPlayer.Movement.UserInput.x;

            // get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _movementPlayer.Movement.CharacterController.radius, Vector3.down, out hitInfo, _movementPlayer.Movement.CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            _movementPlayer.Movement.Direction.x = desiredMove.x * _movementPlayer.SpeedsValue.Current;
            _movementPlayer.Movement.Direction.z = desiredMove.z * _movementPlayer.SpeedsValue.Current;
        }
		
        private void Move()
        {
        	_movementPlayer.Movement.Collision = _movementPlayer.Movement.CharacterController.Move(_movementPlayer.Movement.Direction * Time.fixedDeltaTime);
        }

        private void GetPlayerInput()
        {
        	var verticalAxes = (int)_inputKeys.MoveForward();
            var horizontalAxes = (int)_inputKeys.MoveRight();
            
          	_movementPlayer.Movement.UserInput = new Vector2(horizontalAxes, verticalAxes);
	        
            if (_movementPlayer.Movement.UserInput.sqrMagnitude > 1)
            {
                _movementPlayer.Movement.UserInput.Normalize();
            }
        }
	}
}

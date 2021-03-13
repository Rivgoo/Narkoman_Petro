using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey; 
using Random = UnityEngine.Random;
using PlayerData;
using Objects;

namespace Player
{
    internal class PlayerMoveController : MonoBehaviour
    {
		private MovementPlayerData _movementPlayer;
    	private MovementCameraData _movementCamera;
	
        private PlayerSound _sound;
        private EndurancePlayer _endurance;
        
        public void Init(MovementPlayerData movementPlayer, MovementCameraData movementCamera, PlayerSound sound, EndurancePlayer endurance)
        {
        	_movementPlayer = movementPlayer;
        	_movementCamera = movementCamera;
        	_sound = sound;
        	_endurance = endurance;
        }
        
		#region Squatting 
		private void Crouch()
		{
			if (_movementPlayer.State.IsCrouch)
	        {
				_movementPlayer.CharacterController.height = Mathf.Lerp(_movementPlayer.CharacterController.height, _movementPlayer.Crouch.CrouchHeight, Time.fixedDeltaTime * _movementPlayer.Crouch.SpeedDown);
				CrouchCameraDown();
			}
			else
			{
				if (_movementPlayer.CharacterController.height >  _movementPlayer.Crouch.CharacterHeight - 0.005f)
				{
					_movementPlayer.CharacterController.height = _movementPlayer.Crouch.CharacterHeight;
					_movementPlayer.State.UpCharacter = false;
					return;
				}
				
				CrouchCameraUp();
				transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + (_movementPlayer.Crouch.CharacterHeight - _movementPlayer.CharacterController.height), transform.position.z),Time.fixedDeltaTime * _movementPlayer.Crouch.SpeedUp);
				
				_movementPlayer.CharacterController.height = Mathf.Lerp(_movementPlayer.CharacterController.height, _movementPlayer.Crouch.CharacterHeight, Time.fixedDeltaTime * _movementPlayer.Crouch.SpeedUp);
				_movementPlayer.State.UpCharacter = true;

			}
		}
		
		private void CrouchCameraDown()
		{
			var target = new Vector3(_movementCamera.Shake.CameraVector.x, _movementPlayer.Crouch.CameraHeightDown, _movementCamera.Shake.CameraVector.z);
		
			_movementCamera.Shake.CameraVector = Vector3.Lerp(_movementCamera.Shake.CameraVector, target, Time.fixedDeltaTime *  _movementPlayer.Crouch.SpeedDown);
		}
		
		private void CrouchCameraUp()
		{
			var target = new Vector3(_movementCamera.Shake.CameraVector.x, _movementCamera.OriginalCameraPosition.y, _movementCamera.Shake.CameraVector.z);
		
			_movementCamera.Shake.CameraVector = Vector3.Lerp(_movementCamera.Shake.CameraVector, target, Time.fixedDeltaTime *  _movementPlayer.Crouch.SpeedUp);
		}
		
        private void CheckCrouch()
	        {        	
	        	if (Keys.CrouchDown() && !_movementPlayer.State.IsJumping)
	        	{
	        		_movementPlayer.State.IsCrouch = true;
	        		_movementPlayer.Crouch.CheckCrouchRaycast = false; 
	        		return;
	        	} 
	        	else if (Keys.CrouchUp())
	        	{
	        		_movementPlayer.Crouch.CheckCrouchRaycast = true;
	        	}
	        	
	        	if (_movementPlayer.Crouch.CheckCrouchRaycast) 
	        	{
	        		if (Physics.Raycast(transform.position, Vector3.up, _movementPlayer.Crouch.DistanceCheckCrouchRaycast)) 
	        		{
	        			_movementPlayer.State.IsCrouch = true;
	        		}
	        		else
	        		{
	        			_movementPlayer.State.IsCrouch = false;
	        			_movementPlayer.Crouch.CheckCrouchRaycast = false;
	        		}
	        	}
	        }
        #endregion
        
        #region System
        private void Start()
        {
        	_movementPlayer.Gravity.OriginalWorldGravity = Physics.gravity;
        }
        
        private void Update()
        {   
        	CheckMovePlayer(); 
        	CheckCrouch(); 
        	CheckRun();  	 	  	
            EndJumping();
            CheckJump();
            ChooseCurrentTypeSpeed();
            NullDirectionY();
            PreviouslyGrounded();
        } 
        
        private void NullDirectionY()
        {
        	if (!_movementPlayer.CharacterController.isGrounded && !_movementPlayer.State.IsJumping && _movementPlayer.State.PreviouslyGrounded)
            {
                 _movementPlayer.Move.Direction.y = 0f;
            }
        }
        
        private void PreviouslyGrounded()
        {
        	_movementPlayer.State.PreviouslyGrounded = _movementPlayer.CharacterController.isGrounded;
        }
        
        private void FixedUpdate()
        {  
        	UpdateWorldGravity();
            GetPlayerInput();
            
            SetMoveDirection();
            Crouch();
            Jump();
            Move();
            
            UpdateValueEndurance();    
        }
        #endregion
        
        private void CheckRun()
        {
        	_endurance.CheckKeyDownRun(ref _movementPlayer.State.IsRun);
        	_movementPlayer.State.IsRun = _movementPlayer.State.IsRun && !_movementPlayer.State.IsCrouch;
        	
        }
		
        private void EndJumping()
        {
        	if (!_movementPlayer.State.PreviouslyGrounded && _movementPlayer.CharacterController.isGrounded && !_movementPlayer.State.UpCharacter)
            {
                StartCoroutine(_movementCamera.JumpShake.JumpBob.DoBobCycle());
                PlayLandingSound();
                _movementPlayer.Move.Direction.y = 0f;
                _movementPlayer.State.IsJumping = false;
            }
        }
        
        private void SetMoveDirection()
        {          
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * _movementPlayer.Move.Input.y + transform.right * _movementPlayer.Move.Input.x;

            // get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _movementPlayer.CharacterController.radius, Vector3.down, out _movementPlayer.Move.HitInfo, _movementPlayer.CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            
            desiredMove = Vector3.ProjectOnPlane(desiredMove, _movementPlayer.Move.HitInfo.normal).normalized;
            
            _movementPlayer.Move.Direction.x = desiredMove.x * _movementPlayer.SpeedsSettings.CurrentSpeed;
            _movementPlayer.Move.Direction.z = desiredMove.z * _movementPlayer.SpeedsSettings.CurrentSpeed;
        }
		
        private void PlayLandingSound()
        {
        	_sound.PlayLandingSound();
            _movementPlayer.Step.NextStep = _movementPlayer.Step.StepCycle + .5f;
        } 
		
        private void Move()
        {
        	_movementPlayer.Physic.Collision = _movementPlayer.CharacterController.Move(_movementPlayer.Move.Direction * Time.fixedDeltaTime);
        }
		
        private void Jump()
        {
        	if (_movementPlayer.CharacterController.isGrounded)
            {
                _movementPlayer.Move.Direction.y = -_movementPlayer.Gravity.StickToGroundForce;

                if (_movementPlayer.State.IsJump)
                {
                    _movementPlayer.Move.Direction.y = _movementPlayer.Speeds.Jump;
                    
                    _sound.PlayJumpSound();
                    
                    _movementPlayer.State.IsJump = false;
                    _movementPlayer.State.IsJumping = true;
                    
                    _endurance.PlayerJump();
                }
            }
            else
            {
                _movementPlayer.Move.Direction += Physics.gravity *  _movementPlayer.Gravity.GravityForce * Time.fixedDeltaTime;
            }
        }
        
        private void CheckJump()
        {
        	if (!_movementPlayer.State.IsJump && !_movementPlayer.State.IsCrouch && !_movementPlayer.State.UpCharacter)
            {
            	_movementPlayer.State.IsJump = _endurance.CheckIsJump() && _movementPlayer.CharacterController.isGrounded;
            }
        }
                  
        private void UpdateValueEndurance()
        {       	
        	if (_movementPlayer.State.IsMovePlayer) 
            {
            	if (!_movementPlayer.State.IsWalking) // Player Run
            	{
            		_endurance.PlayerRun();
            	}
            	else if (!Keys.Run() && !_movementPlayer.State.IsCrouch) // Don't key down Speed Up
            	{
            		_endurance.RecoveryEnduranceWalk();
            	}  	
            }
        	else if(!_movementPlayer.State.IsCrouch && !Keys.Run())
        	{
        		_endurance.RecoveryEnduranceDefoult();
        	}
        }  

        private void GetPlayerInput()
        {
        	_movementPlayer.Move.VerticalAxes = (int)Keys.MoveForward();
        	_movementPlayer.Move.HorizontalAxes = (int)Keys.MoveRight();
            
            SetInput(ref _movementPlayer.Move.HorizontalAxes, ref _movementPlayer.Move.VerticalAxes);
        }
		
        private void SetInput(ref float horizontal, ref float vertical)
        {
        	_movementPlayer.Move.Input = new Vector2(horizontal, vertical);
	        
            if (_movementPlayer.Move.Input.sqrMagnitude > 1)
            {
                _movementPlayer.Move.Input.Normalize();
            }
        }
        
        private void ChooseCurrentTypeSpeed()
        {   
        	_movementPlayer.State.IsWalking = !(_endurance.CheckIsSpeedUp(_movementPlayer.State.IsRun));
        	
        	float inputSpeed = 0;
        	
        	if (_movementPlayer.State.IsCrouch) 
        	{
        		inputSpeed = _movementPlayer.Speeds.Crouch;
        	}
        	else if(_movementPlayer.Move.Input == Vector2.zero)
        	{
        		inputSpeed = 0;        	
        	}
        	else if (_movementPlayer.State.IsWalking)
        	{
        		inputSpeed = _movementPlayer.Speeds.Walk;
        	}
        	else
        	{
        		inputSpeed = _movementPlayer.Speeds.Run;
        	}
        	
        	_movementPlayer.SpeedsSettings.CurrentSpeed = Mathf.Lerp(_movementPlayer.SpeedsSettings.CurrentSpeed, inputSpeed, Time.fixedDeltaTime * _movementPlayer.SpeedsSettings.SpeedTransition);
        	
        	if (_movementPlayer.SpeedsSettings.CurrentSpeed < 0.03) 
        	{
        		_movementPlayer.SpeedsSettings.CurrentSpeed = 0;
        	}
        }
        
        private void CheckMovePlayer()
        {
        	_movementPlayer.State.IsMovePlayer = _movementPlayer.CharacterController.velocity.magnitude > 0; 
        }
        
        private void UpdateWorldGravity()
        {
        	Physics.gravity = _movementPlayer.Gravity.WorldGravity + _movementPlayer.Gravity.OriginalWorldGravity;
        }
        // Player Physics
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
			  
            if (body == null || hit.moveDirection.y < -0.3f || _movementPlayer.Physic.Collision == CollisionFlags.Below) return;
            
            var pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
            //body.velocity = pushDir * _force;
            body.AddForce( pushDir * _movementPlayer.Physic.Force, ForceMode.Force);
        }
    }
    
}

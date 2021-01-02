using UnityEngine;
using Keys = PlayerInputKeys.InputKeys.CheckKey;  
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof (CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
    	
    	[Header("State Move Player")]
    	[SerializeField]  private bool _isWalking;
    	[SerializeField]  private bool _isMovePlayer;
      [SerializeField]  private bool _isSpeedUp;
      [SerializeField]  private bool _isSquatting;
      [SerializeField]  private bool _isJumping;
        
//        [Header("State Move Player")]
//    	[SerializeField] [ReadOnly] private bool _isWalking;
//    	[SerializeField] [ReadOnly] private bool _isMovePlayer;
//        [SerializeField] [ReadOnly] private bool _isSpeedUp;
//        [SerializeField] [ReadOnly] private bool _isSquatting;
//        [SerializeField] [ReadOnly] private bool _isJumping;
    	
        [Header("Speeds")]
        [SerializeField] private PlayerSpeeds _speeds;
        
        [Header("Step")]
        [SerializeField] [Range(0, 3f)] private float _runStepLenghten;
        [SerializeField] [Range(0, 3f)] private float _walkStepLenghten;
        [SerializeField] [Range(0, 3f)] private float _squattingStepLenghten;
        [SerializeField] private float _stepInterval;
       
        [Header("Gravity")]
        [SerializeField] private float _stickToGroundForce;
        [SerializeField] private float _gravity; 
        
        [Header("Camera Rotation")]
        [SerializeField] private MouseLook _mouseLook; 
        
        [Header("Shaking The Camera While Walking ")]
        [SerializeField] private CurveControlledBob _headBob = new CurveControlledBob();
        [Space]
        [SerializeField] private float _bobRangeRun;
        [SerializeField] private float _bobRangeNotMoveing;
        [SerializeField] private float _bobRangeSquatting;
        
        [Header("Jump Lerp")]
        [SerializeField] private LerpControlledBob _jumpBob = new LerpControlledBob();
		
        [Header("Main Camera")]
        [SerializeField] private Transform _camera;
        
        [Header("Character Controller")]
        [SerializeField] private CharacterController _characterController;
		
        [Header("Player Sound")]
        [SerializeField] private PlayerSound _sound;
        
        [Header("Player Characteristics")]
        [SerializeField] private EndurancePlayer _endurance;
        
        [Header("Squatting")] 
        [SerializeField] private Vector3 _valueSquatting;
        [SerializeField] private float _speedShakeSquatting;

        [Header("Player Physics")]
        [SerializeField] private float _force;
        
        private Vector2 _input;
        private Vector3 _moveDirection = Vector3.zero;
        private Vector3 _originalCameraPosition;
        private Vector3 _cameraVector;
        private Vector3 _tempCameraPosition;
        
        private float _stepCycle;
        private float _nextStep;
        private float _currentSpeed;
        
        private float _verticalAxes;
        private float _horizontalAxes;
        
        private bool _isJump;
        private bool _previouslyGrounded;
        
        private RaycastHit _hitInfo;
        
        
        private void DownCameraSquatting()
        {
        	if (_isSquatting)
        	{
       			_cameraVector = Vector3.Slerp(_cameraVector,  _valueSquatting , Time.deltaTime * _speedShakeSquatting);
			}
        	else
        	{
        		_cameraVector = Vector3.Slerp(_cameraVector, Vector3.zero, Time.deltaTime * _speedShakeSquatting);
        	}
        }     
        
        private void Squatting()
        {
        	if (Keys.SquattingDown() && !_isJumping)
        	{
        		_isSquatting = true;
        	}
        	else if (Keys.SquattingUp())
        	{
        		_isSquatting = false;
        	}
        }
        
        private void Start()
        {
            _originalCameraPosition = _camera.localPosition;
            
            _headBob.Setup(_camera, _stepInterval);
            
            _stepCycle = 0f;
            _nextStep = _stepCycle/2f;
            _isJumping = false;
            
			_mouseLook.Init(transform , _camera);
			
			MouseLook.HideCursor();
        }
        
        private void CheckSpeedUp()
        {
        	_endurance.CheckKeyDownSpeedUp(ref _isSpeedUp);
        	_isSpeedUp = _isSpeedUp && !_isSquatting;
        	
        }
        
        private void Update()
        {   
        	CheckMovePlayer(); 
        	Squatting(); 
        	CheckSpeedUp();  	
        	DownCameraSquatting();        	
        	HeadBobRun();        	
            RotateView();
            EndJumping();
            CheckJump();
            
            if (!_characterController.isGrounded && !_isJumping && _previouslyGrounded)
            {
                _moveDirection.y = 0f;
            }

            _previouslyGrounded = _characterController.isGrounded;
            
            MouseLook.CursoreLockUpdate(); //TODO: When will Add Menu UI then delete this method
        }  
		
        private void EndJumping()
        {
        	if (!_previouslyGrounded && _characterController.isGrounded)
            {
                StartCoroutine(_jumpBob.DoBobCycle());
                PlayLandingSound();
                _moveDirection.y = 0f;
                _isJumping = false;
            }
        }
        
        private void HeadBobRun()
        {
        	if (_currentSpeed == _speeds.Run)
        	{
        		_headBob._bobRange = _bobRangeRun;
        	}
        	else if (_isSquatting)
            {
            	_headBob._bobRange = -_bobRangeSquatting;
            }
        	else
        	{
        		_headBob._bobRange = 0;
        	}
        }
        
        private void FixedUpdate()
        {  
            GetPlayerInput();
            
            ChooseCurrentTypeSpeed();
            
            SetMoveDirection();
            
            Jump();
            
            Move();

            ProgressStepCycle(_currentSpeed);
            UpdateCameraPosition(_currentSpeed);  
            
            UpdateValueEndurance();    
        }
        
        private void SetMoveDirection()
        {          
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*_input.y + transform.right*_input.x;

            // get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out _hitInfo, _characterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            
            desiredMove = Vector3.ProjectOnPlane(desiredMove, _hitInfo.normal).normalized;
            
            _moveDirection.x = desiredMove.x*_currentSpeed;
            _moveDirection.z = desiredMove.z*_currentSpeed;
        }
		
        private void PlayLandingSound()
        {
        	_sound.PlayLandingSound();
            _nextStep = _stepCycle + .5f;
        }
        
        private void PlayFootStepAudio()
        {
            if (_characterController.isGrounded)
            {
               _sound.PlayFootStepAudio();
            }
        } 
		
        private void Move()
        {
        	_characterController.Move(_moveDirection * Time.fixedDeltaTime);
        }
		
        private void Jump()
        {
        	if (_characterController.isGrounded)
            {
                _moveDirection.y = -_stickToGroundForce;

                if (_isJump)
                {
                    _moveDirection.y = _speeds.Jump;
                    
                    _sound.PlayJumpSound();
                    
                    _isJump = false;
                    _isJumping = true;
                    
                    _endurance.PlayerJump();
                }
            }
            else
            {
                _moveDirection += Physics.gravity * _gravity * Time.fixedDeltaTime;
            }
        }
        
        private void CheckJump()
        {
        	if (!_isJump && !_isSquatting)
            {
            	_isJump = _endurance.CheckIsJump() && _characterController.isGrounded;
            }
        }
                  
        private void UpdateValueEndurance()
        {       	
        	if (_isMovePlayer) 
            {
            	if (!_isWalking) // Player Run
            	{
            		_endurance.PlayerRun();
            	}
            	else if (!_isSpeedUp && !_isSquatting) // Don't key down Speed Up
            	{
            		_endurance.RecoveryEnduranceWalk();
            	} 
            	
            }
        	else if(!_isSquatting)
        	{
        		_endurance.RecoveryEnduranceDefoult();
        	}
        }
                 
        private void ProgressStepCycle(float speed) // old method first person controller
        {
            if (_isMovePlayer && (_input.x != 0 || _input.y != 0))
            {
            	_stepCycle += (_characterController.velocity.magnitude + (speed*(_isSquatting ? _squattingStepLenghten : _isWalking ?_walkStepLenghten : _runStepLenghten))) * Time.fixedDeltaTime;
            }

            if (!(_stepCycle > _nextStep))
            {
                return;
            }

            _nextStep = _stepCycle + _stepInterval;

            PlayFootStepAudio();
        }
		
        private void UpdateCameraPosition(float speed)
        {
            _tempCameraPosition = _camera.transform.localPosition;

            if (_characterController.velocity.magnitude > 0 && _characterController.isGrounded || !_isMovePlayer)
            {    
				if (!_isMovePlayer)
				{
					_headBob._bobRange = -_bobRangeNotMoveing;
				} 

				_camera.localPosition = _headBob.DoHeadBob(_characterController.velocity.magnitude + (speed * _walkStepLenghten));

                _tempCameraPosition =  _camera.localPosition;
                _tempCameraPosition.y = _camera.localPosition.y - _jumpBob.Offset();
            }
            else
            {
                _tempCameraPosition.y = _originalCameraPosition.y - _jumpBob.Offset();
            }
            
            _tempCameraPosition += _cameraVector;
            
            _camera.localPosition =  _tempCameraPosition;
        }

        private void GetPlayerInput()
        {
        	_verticalAxes = Keys.MoveForward();				
            _horizontalAxes = Keys.MoveRight();
            
            SetInput(ref _horizontalAxes,ref _verticalAxes);
        }
		
        private void SetInput(ref float horizontal, ref float vertical)
        {
        	_input = new Vector2(horizontal, vertical);
	        
            if (_input.sqrMagnitude > 1)
            {
                _input.Normalize();
            }
        }
        
        private void ChooseCurrentTypeSpeed()
        {
        	_isWalking = !(_endurance.CheckIsSpeedUp(_isSpeedUp) && !_isSquatting);

        	_currentSpeed = _isSquatting ? _speeds.Squatting : _isWalking ? _speeds.Walk : _speeds.Run;
        }
        
        private void CheckMovePlayer()
        {
        	_isMovePlayer = _characterController.velocity.magnitude > 0; //(horizontal != 0 || vertical != 0) && _characterController.isGrounded;
        }
        
        private void RotateView()
        {
            _mouseLook.LookRotation (transform, _camera.transform);
        }
        
        // Player Physics
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (body == null || hit.moveDirection.y < -0.9f) return;
            
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            
            body.velocity = pushDir * _force;
        }
    }
}

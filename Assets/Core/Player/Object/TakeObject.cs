using UnityEngine;
using Mouse = PlayerInput.InputKeys.CheckMouseButton; 
using PlayerInput;
using Player;
using PlayerData;

namespace Objects
{
	public class TakeObject : MonoBehaviour
	{	
		[Header("Position Take")]
		[SerializeField] private Transform _transformTakedObject;
		
		[Header("Min Distancy to Object for Take On")]
		[SerializeField] private float _maxDistancyToTake;
		
		[Header("Speeds")]
		[SerializeField] private float _speedTake;
		[SerializeField] private float _speedRotation;
		
		[Header("Max Distancy to Object Before Take Off")]
		[SerializeField] private float _maxDistancyToObject;
		
		[Header("Force Throw Object")]
		[SerializeField] private float _forceThrowAway;
		
		[Header("Speed Force After Take Off")]
		[SerializeField] private float _forseAfterTakeOff;
		[SerializeField] private float _forseTorqueAfterTakeOff;
		
		private PlayerSpeeds _speeds;
		
		private IPhysicObject _physicObject;
		
		private bool _isTake { get {return _physicObject != null;}}
		private bool _isTakeOff;
		
		private CollisionDetectionMode _originalMode;
		private RotationAxeses _rotatinAxes = RotationAxeses.X;
		private Vector3 _vectorRotation = Vector3.right;
		
		private MouseLook _cameraRotation;
		private RaycastHit _hit;
		
		private float _maxDownCameraRotation;
		
		private MovementPlayerData _editPlayerData;
		private MovementPlayerData _originalPlayerData;
		
		public void Init(MouseLook cameraRotation, MovementPlayerData editPlayerData,  MovementPlayerData originalPlayerData)
		{
			_cameraRotation = cameraRotation;
			_editPlayerData = editPlayerData;
			_originalPlayerData = originalPlayerData;
		}

		private void UpdatePlayerSpeeds()
		{
			_editPlayerData.Speeds.Jump -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Jump, _speeds.Jump), 0, 100);
			_editPlayerData.Speeds.Run -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Run, _speeds.Run), 0, 100);
			_editPlayerData.Speeds.Walk -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Walk, _speeds.Walk), 0, 100);
			_editPlayerData.Speeds.Crouch -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Crouch, _speeds.Crouch), 0, 100);
		}
		
		private void RecoverPlayerSpeeds()
		{
			_editPlayerData.Speeds.Jump += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Jump, _speeds.Jump), 0, 100);
			_editPlayerData.Speeds.Run += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Run, _speeds.Run), 0, 100);
			_editPlayerData.Speeds.Walk += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Walk, _speeds.Walk), 0, 100);
			_editPlayerData.Speeds.Crouch += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Crouch, _speeds.Crouch), 0, 100);
		}
		
		private void CheckTake()
		{
			if (Mouse.Down(MouseButtons.RightButton) && !_isTake)
			{
				TakeOn();
			}
			
			if(Mouse.Up(MouseButtons.RightButton) && _isTake)
			{
				TakeOff();
			}
		}
		
		private void TakeOff()
		{
			AddForceAfterTakeOff();
			RecoverPlayerSpeeds();
			
			_physicObject.IsTake = false;
			_isTakeOff = true;
			_physicObject.Rigidbody.collisionDetectionMode = _originalMode;
			
			_cameraRotation.SetDownCameraRotatin(-_maxDownCameraRotation);
			
			_maxDownCameraRotation = 0;
			
			_physicObject = null;
		}
		
		private void TakeOn()
		{
			CheckIsObjectRigidbody();
			
			if (_isTake)
			{
				_isTakeOff = false;
				_physicObject.IsTake = true;
				_speeds = _physicObject.Speeds;
				_originalMode = _physicObject.Rigidbody.collisionDetectionMode;
				_physicObject.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
				_maxDownCameraRotation = _physicObject.MaxDownCameraRotation;
				
				UpdatePlayerSpeeds();
								
				 _cameraRotation.SetDownCameraRotatin(_maxDownCameraRotation);
			}
		}
		
		private void CheckIsObjectRigidbody()
		{
			var mouseVector = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(mouseVector, out _hit, _maxDistancyToTake))
			{
				_physicObject = _hit.collider.gameObject.GetComponent(typeof(IPhysicObject)) as IPhysicObject;
				
				if (_physicObject != null)
				{ 
					_transformTakedObject.localPosition = _physicObject.PositionTakedObject;
				}
			}
		}
		
		private void AddForceAfterTakeOff()
		{
			var directionForce = _transformTakedObject.position - _physicObject.Rigidbody.position;
			_physicObject.Rigidbody.AddForce((directionForce) * _forseAfterTakeOff, ForceMode.Impulse);
			
			_physicObject.Rigidbody.AddTorque((GetRandomVectorTorque(_physicObject.MaxVectorTorque, directionForce) * _forseTorqueAfterTakeOff), ForceMode.Impulse);
		}
		
		private void UpdateObjectPosition()
		{
			if (Mouse.Stay(MouseButtons.RightButton) && _isTake) 
			{
				_physicObject.Rigidbody.MovePosition( Vector3.Lerp(_physicObject.Rigidbody.position, _transformTakedObject.position, _speedTake * Time.fixedDeltaTime - (_physicObject.Rigidbody.mass * 0.02f) ));
				_physicObject.Rigidbody.velocity = Vector3.zero;
				
				if (CheckDistancyToObject()) 
				{
					TakeOff();
				}
			}
		}
		
		private bool CheckDistancyToObject()
		{
			var distancy = Mathf.Sqrt( Mathf.Pow(_physicObject.Rigidbody.position.x - _transformTakedObject.position.x, 2) +  Mathf.Pow(_physicObject.Rigidbody.position.y - _transformTakedObject.position.y, 2) +  Mathf.Pow(_physicObject.Rigidbody.position.z - _transformTakedObject.position.z, 2));
			
			return distancy > _maxDistancyToObject;
		}
		
		private void TakeOffYouself()
		{
			_cameraRotation.SetDownCameraRotatin(-_maxDownCameraRotation);
			
			_maxDownCameraRotation = 0;
			RecoverPlayerSpeeds();
		}
		
		private void CheckNullObject()
		{
			if (!_isTake && !_isTakeOff)
			{
				TakeOffYouself();
			}
		}
			
		private void Update()
		{
			CheckNullObject();
			
			CheckTake();
			CheckRotationAxises();
			RotationObject();
			CheckThrowAwayObject();
		}
		
		private void CheckThrowAwayObject()
		{
			if (Mouse.Down(MouseButtons.LeftButton) && _isTake)
			{
				ThrowAwayObject();
			}
		}
		
		private void ThrowAwayObject()
		{
			_physicObject.Rigidbody.AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * _forceThrowAway, ForceMode.Impulse);
			TakeOff();
		}
		
		private void FixedUpdate()
		{
			UpdateObjectPosition();
		}
		
		private void CheckRotationAxises()
		{
			if (Mouse.Down(MouseButtons.ScrollLock)) 
			{
				_rotatinAxes++;
				
				if ((int)_rotatinAxes >= 3)
				{
					_rotatinAxes = 0;
				}
				
				if (_rotatinAxes == RotationAxeses.X)
				{
					_vectorRotation = Vector3.right;
				}
				else if(_rotatinAxes == RotationAxeses.Y)
				{
					_vectorRotation = Vector3.up;
				}
				else
				{
					_vectorRotation = Vector3.forward;
				}
			}	
		}
		
		private void RotationObject()
		{
			if (_isTake)
			{
				if (Input.GetAxis("Mouse ScrollWheel") != 0)
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0)
					{
						_physicObject.Rigidbody.AddTorque(_vectorRotation * _speedRotation, ForceMode.Force);
					}
					else
					{
						_physicObject.Rigidbody.AddTorque((_vectorRotation * -1) * _speedRotation, ForceMode.Force);
					}
				}
				
				if (Input.GetKeyDown("f1"))
				{
					_physicObject.Rigidbody.angularVelocity = Vector3.zero;
				}
			}
		}
		
		private enum RotationAxeses
		{
			X, Y, Z
		}
		
		private Vector3 GetRandomVectorTorque(Vector3 target, Vector3 directionNormalize)
		{
			return new Vector3(Random.Range(-target.x, target.x) * directionNormalize.x, Random.Range(-target.y, target.y) * directionNormalize.y, Random.Range(-target.z, target.z) * directionNormalize.z);
		}
	}
}

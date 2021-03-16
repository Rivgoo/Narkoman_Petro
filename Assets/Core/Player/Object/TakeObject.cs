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
		[SerializeField] private float _speedMove;
		[SerializeField] private float _speedMoveWhenPlayerRun;
		[SerializeField] private float _speedRotation;
		
		private DefoultPhysicObjectData _dataObject;
		
		private IPhysicObject _physicObject;
		
		private bool _isTake { get {return _physicObject != null;}}
		private bool _isTakeOff;
		
		private CollisionDetectionMode _originalMode;
		private RotationAxeses _rotatinAxes = RotationAxeses.X;
		private Vector3 _vectorRotation = Vector3.right;
		
		private MouseLook _cameraRotation;
		private RaycastHit _hit;
		
		private MovementPlayerData _editPlayerData;
		private MovementPlayerData _originalPlayerData;
		
		private EndurancePlayer _endurancy;
		
		private Quaternion _beginRotationVector;
		
		private float _distancy;
		private bool _isRecoveryed;
		
		public void Init(MouseLook cameraRotation, EndurancePlayer endurancy, MovementPlayerData editPlayerData,  MovementPlayerData originalPlayerData)
		{
			_cameraRotation = cameraRotation;
			_editPlayerData = editPlayerData;
			_originalPlayerData = originalPlayerData;
			_endurancy = endurancy;
		}
		
		private void SetEndurancy()
		{
			if (_isTake && _distancy > 0.035)
			{
				_endurancy.SetTakeObjectValueDropRate(_physicObject.DataObject.DropRateEndurancy);
			}
		}
		
		private void UpdatePlayerSpeeds()
		{
			_editPlayerData.Speeds.Jump -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Jump, _dataObject.Speeds.Jump), 0, 100);
			_editPlayerData.Speeds.Run -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Run, _dataObject.Speeds.Run), 0, 100);
			_editPlayerData.Speeds.Walk -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Walk, _dataObject.Speeds.Walk), 0, 100);
			_editPlayerData.Speeds.Crouch -= Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Crouch, _dataObject.Speeds.Crouch), 0, 100);
			_isRecoveryed = false;
		}
		
		private void RecoverPlayerSpeeds()
		{
			if (!_isRecoveryed)
			{
				_editPlayerData.Speeds.Jump += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Jump, _dataObject.Speeds.Jump), 0, 100);
				_editPlayerData.Speeds.Run += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Run, _dataObject.Speeds.Run), 0, 100);
				_editPlayerData.Speeds.Walk += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Walk, _dataObject.Speeds.Walk), 0, 100);
				_editPlayerData.Speeds.Crouch += Mathf.Clamp(CalculationNumber.GetNumber(_originalPlayerData.Speeds.Crouch, _dataObject.Speeds.Crouch), 0, 100);
				_isRecoveryed = true;
			}
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
			
			_isTakeOff = true;
			_physicObject.DataObject.Rigidbody.collisionDetectionMode = _originalMode;
			
			_cameraRotation.SubtractOffsetValueRatation(_physicObject.DataObject.MaxCameraRoation);
			
			_physicObject = null;
		}
		
		private void SetTakePostion()
		{
			if (_physicObject.DataObject.TypeTakePosition == TypeTakePosition.Player)
			{
				 _transformTakedObject.localPosition = _physicObject.DataObject.PositionTakedObject;
			}
			else
			{
				 _transformTakedObject.position =_physicObject.DataObject.Rigidbody.position;
			}
		}
		
		private void TakeOn()
		{
			CheckIsObjectRigidbody();
			
			if (_isTake && _endurancy.CheckIsTake())
			{
				
				_isTakeOff = false;
				SetTakePostion();
				_originalMode = _physicObject.DataObject.Rigidbody.collisionDetectionMode;
				_physicObject.DataObject.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				_dataObject.MaxCameraRoation = _physicObject.DataObject.MaxCameraRoation;
				_beginRotationVector = _physicObject.DataObject.Rigidbody.rotation;
				
				UpdatePlayerSpeeds();
								
				_cameraRotation.AddOffsetValueRatation(_physicObject.DataObject.MaxCameraRoation);
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
					var distancy = Vector3.Distance(transform.position, _physicObject.DataObject.Rigidbody.position);
					
					if (distancy < _physicObject.DataObject.MaxDistancyToTake)
					{ 
						_dataObject = _physicObject.DataObject;
					}
					else
					{
						_physicObject = null;
					}
				}
			}
		}
		
		private void AddForceAfterTakeOff()
		{		
			var directionForce = _transformTakedObject.position - _physicObject.DataObject.Rigidbody.position;
			_physicObject.DataObject.Rigidbody.AddForce((directionForce) * _physicObject.DataObject.ForseAfterTakeOff, ForceMode.Impulse);
			
			_physicObject.DataObject.Rigidbody.AddTorque((GetRandomVectorTorque(_physicObject.DataObject.MaxVectorTorque, directionForce) * _physicObject.DataObject.ForseTorqueAfterTakeOff), ForceMode.Impulse);
		}
		
		private void UpdateObjectPosition()
		{
			if (!_endurancy.CheckIsTaking() && _isTake)
			{
				TakeOff();
				return;
			}
			
			if (Mouse.Stay(MouseButtons.RightButton) && _isTake) 
			{
				var speed = _speedMove;
				
				if (_editPlayerData.State.IsRun)
				{
					speed = _speedMoveWhenPlayerRun;
				}
				
				var direction = Vector3.Lerp(_physicObject.DataObject.Rigidbody.position, _transformTakedObject.position, (speed * Time.fixedDeltaTime) / (_physicObject.DataObject.SlowingMove) );
				
				if (_physicObject.DataObject.BlockMoveY) 
				{
					direction = new Vector3(direction.x, _physicObject.DataObject.Rigidbody.position.y ,direction.z);
				}
				
				_physicObject.DataObject.Rigidbody.MovePosition(direction);
				
				_physicObject.DataObject.Rigidbody.velocity = Vector3.zero;
				
				if (CheckDistancyToObject()) 
				{
					TakeOff();
				}
			}
		}
		
		private bool CheckDistancyToObject()
		{
			_distancy = Vector3.Distance(_transformTakedObject.position, _physicObject.DataObject.Rigidbody.position);
			
			return _distancy > _physicObject.DataObject.MaxDistancyToObject;
		}
		
		private void TakeOffYouself()
		{
			_cameraRotation.SubtractOffsetValueRatation(_dataObject.MaxCameraRoation);
						
			RecoverPlayerSpeeds();
			_isTakeOff = true;
		}
		
		private void CheckNullObject()
		{
			if (!_isTake && !_isTakeOff)
			{
				TakeOffYouself();
			}
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
			_physicObject.DataObject.Rigidbody.AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * _physicObject.DataObject.ForceThrowAway, ForceMode.Impulse);
			TakeOff();
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
				if (_physicObject.DataObject.BlockFreeRotation)
				{
					_physicObject.DataObject.Rigidbody.rotation = _beginRotationVector;
				}
				else
				{
					if (Input.GetAxis("Mouse ScrollWheel") != 0 && !_physicObject.DataObject.BlockPlayerRotation)
					{
						if (Input.GetAxis("Mouse ScrollWheel") > 0)
						{
							_physicObject.DataObject.Rigidbody.AddTorque(_vectorRotation * _speedRotation, ForceMode.Force);
						}
						else
						{
							_physicObject.DataObject.Rigidbody.AddTorque((_vectorRotation * -1) * _speedRotation, ForceMode.Force);
						}
					}
					else
					{
						_physicObject.DataObject.Rigidbody.angularVelocity = Vector3.zero;
					}
				}
			}
		}
		
		private enum RotationAxeses
		{
			X, Y, Z
		}
		
		private void FixedUpdate()
		{
			UpdateObjectPosition();
			SetEndurancy();
		}
		
		private void Update()
		{
			CheckNullObject();
			
			CheckTake();
			
			// ROTATION OBJECT
			//CheckRotationAxises();
			//RotationObject();
			
			CheckThrowAwayObject();
		}
		
		private Vector3 GetRandomVectorTorque(Vector3 target, Vector3 directionNormalize)
		{
			return new Vector3(Random.Range(-target.x, target.x) * directionNormalize.x, Random.Range(-target.y, target.y) * directionNormalize.y, Random.Range(-target.z, target.z) * directionNormalize.z);
		}
	}
}

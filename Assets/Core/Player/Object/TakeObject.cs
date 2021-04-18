using UnityEngine;
using Mouse = PlayerInput.InputKeys.CheckMouseButton; 
using PlayerInput;
using Player;
using PlayerData;
using System.Collections;

namespace Objects
{
	public class TakeObject : MonoBehaviour
	{	
		[SerializeField] private Transform _transformTakedObject;
		
		[Space]
		[SerializeField] private float _maxDistancyToTake;
		
		[SerializeField] private float _speedMove;
		
		private PhysicObjectData _dataObject;
		
		private IPhysicObject _physicObject;
		
		private bool _isTake { get {return _physicObject != null;}}
		private bool _isTakeOff;
		
		private MouseLook _cameraRotation;
		private RaycastHit _hit;
		
		private MovementPlayerData _editPlayerData;
		 
		private EditPlayerMovementData _editData;
		
		private EndurancePlayer _endurancy;
		
		private PlayerSpeeds _editPlayerSpeeds;
		private PlayerSpeeds _originalPlayerSpeeds;
		
		
		private Quaternion _beginRotationVector;
		
		private float _distancy;
		private Vector3 _direction;
		private bool _isRecoveryed;
		
		private bool _isStayDownKeyGet;
		
		public void Init(MouseLook cameraRotation, EndurancePlayer endurancy, EditPlayerMovementData editData, MovementPlayerData editPlayerData)
		{
			_editData = editData;
			_cameraRotation = cameraRotation;
			_editPlayerData = editPlayerData;
			_endurancy = endurancy;
		}
		
		#region Endurancy
		
		private void SetEndurancy()
		{
			if (_isTake && _distancy > 0.035f)
			{
				_endurancy.SetTakeObjectValueDropRate(_physicObject.DataObject.SettingsForPlayer.DropRateEndurancy);
			}
		}
		
		private void UpdatePlayerSpeeds()
		{
			_editPlayerSpeeds.Jump = CalculationNumber.GetNumber(_originalPlayerSpeeds.Jump, _dataObject.SettingsForPlayer.Speeds.Jump);
			_editPlayerSpeeds.Run = CalculationNumber.GetNumber(_originalPlayerSpeeds.Run, _dataObject.SettingsForPlayer.Speeds.Run);
			_editPlayerSpeeds.Walk = CalculationNumber.GetNumber(_originalPlayerSpeeds.Walk, _dataObject.SettingsForPlayer.Speeds.Walk);
			_editPlayerSpeeds.Crouch = CalculationNumber.GetNumber(_originalPlayerSpeeds.Crouch, _dataObject.SettingsForPlayer.Speeds.Crouch);
		}
		
		private void SubstructPlayerSpeeds()
		{
			UpdatePlayerSpeeds();
			_editData.SubstructSpeeds(_editPlayerSpeeds);
			
			_isRecoveryed = false;
		}
		
		private void RecoverPlayerSpeeds()
		{
			if (!_isRecoveryed)
			{
				UpdatePlayerSpeeds();
				_editData.AddSpeeds(_editPlayerSpeeds);
				_isRecoveryed = true;
			}
		}
		
		#endregion
		 
		#region Take
		
		private void TakeOff()
		{
			ResetObjectMass();
						
			_isTakeOff = true;
			
			_cameraRotation.SubtractOffsetValueRatation(_physicObject.DataObject.SettingsForPlayer.ClampAngle);
			
			RecoverPlayerSpeeds();
			
			_physicObject.TakeOff();
			_physicObject = null;
		}
		
		private void TakeOn()
		{
			CheckIsObjectRigidbody();
			
			if (_isTake && _endurancy.CheckIsTake())
			{	
				_physicObject.TakeOn();
				
				_isTakeOff = false;
				
				_cameraRotation.AddOffsetValueRatation(_physicObject.DataObject.SettingsForPlayer.ClampAngle);
				
				//Set posotion Take
				_transformTakedObject.position = _physicObject.GetObjectPosition();
				
				//Save begin value rotation Object
				_beginRotationVector = _physicObject.DataObject.Rigidbody.rotation;
				
				//Copy value speeds
				_originalPlayerSpeeds = _editPlayerData.Speeds;
				
				SubstructPlayerSpeeds();
			}
		}
		
		#endregion
		
		#region Check Object
		
		private void CheckTake()
		{
			if (Mouse.Down(MouseButtons.RightButton) && !_isTake)
			{
				TakeOn();
			}
			
			if(Mouse.Up(MouseButtons.RightButton) && _isTake)
			{
				AddForcesAfterTakeOff();
				TakeOff();
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
		
		private bool CheckDistancyToObject()
		{
			_distancy = Vector3.Distance(_transformTakedObject.position, _physicObject.DataObject.Rigidbody.position);
			
			return _distancy > _physicObject.DataObject.MaxDistancyToObject;
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
		
		private void CheckBlockRotationObject()
		{
			if (_isTake && _physicObject.DataObject.ObjectMovement.BlockFreeRotation)
			{
				_physicObject.DataObject.Rigidbody.rotation = _beginRotationVector;
				
			}
		}
		
		#endregion
		
		#region Object
		
		private void AddForcesAfterTakeOff()
		{	
			ResetObjectMass();
			
			var directionForce = _transformTakedObject.position - _physicObject.DataObject.Rigidbody.position;
			
			_physicObject.DataObject.Rigidbody.AddForce(directionForce * _physicObject.DataObject.ForceTakeOff.ForseAfterTakeOff, ForceMode.Impulse);

			_physicObject.DataObject.Rigidbody.AddTorque((GetRandomVectorTorque(_physicObject.DataObject.ForceTakeOff.MaxVectorTorque, directionForce) * _physicObject.DataObject.ForceTakeOff.ForseTorqueAfterTakeOff), ForceMode.Impulse);
		}
		
		private void UpdateObjectPosition()
		{
			if (_isTake)
			{
				if (Mouse.Stay(MouseButtons.RightButton)) 
				{		
					GetDirection();
					BlockMoveY();
					MoveRigidBody();
					
					if (CheckDistancyToObject() || _endurancy.CheckIsTaking()) 
					{
						AddForcesAfterTakeOff();
						TakeOff();
					}
				}
			}
		}
		
		private void GetDirection()
		{
			var speed = ((_speedMove + _editPlayerData.Speeds.Current * 6f) * Time.fixedDeltaTime) / (_physicObject.DataObject.ObjectMovement.SlowingMove);

			_direction = Vector3.Lerp(_physicObject.DataObject.Rigidbody.position, _transformTakedObject.position, speed);
		}
		
		private void MoveRigidBody()
		{
			_physicObject.Move(_direction);
		}
		
		private void BlockMoveY()
		{
			if (_physicObject.DataObject.ObjectMovement.BlockMoveY) 
			{
				_direction = new Vector3(_direction.x, _physicObject.DataObject.Rigidbody.position.y , _direction.z);
			}
		}
		
		private void TakeOffYouself()
		{
			_physicObject =  null;
			
			_cameraRotation.SubtractOffsetValueRatation(_dataObject.SettingsForPlayer.ClampAngle);
						
			RecoverPlayerSpeeds();
			_isTakeOff = true;
		}
		
		private void ThrowAwayObject()
		{
			ResetObjectMass();
			
			_physicObject.DataObject.Rigidbody.AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * _physicObject.DataObject.ForceTakeOff.ForceThrowAway, ForceMode.Impulse);
			
			AddForcesAfterTakeOff();
			TakeOff();
		}
		
		private void ResetObjectMass()
		{
			_physicObject.DataObject.Rigidbody.mass = _physicObject.DataObject.ObjectMovement.OriginalMassaObject;
		}
		
		#endregion
		

		
		private void FixedUpdate()
		{
			CheckNullObject();
			
			UpdateObjectPosition();
			
			CheckBlockRotationObject();
			
			SetEndurancy();
		}
		
		private void Update()
		{	
			CheckTake();
			
			CheckThrowAwayObject();
		}
		
		private Vector3 GetRandomVectorTorque(Vector3 target, Vector3 directionNormalize)
		{
			return new Vector3(Random.Range(-target.x, target.x) * directionNormalize.x, Random.Range(-target.y, target.y) * directionNormalize.y, Random.Range(-target.z, target.z) * directionNormalize.z);
		}
	}
}

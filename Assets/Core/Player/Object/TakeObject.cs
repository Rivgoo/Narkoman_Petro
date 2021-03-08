using UnityEngine;
using Mouse = PlayerInput.InputKeys.CheckMouseButton; 
using PlayerInput;
using Player;

namespace Objects
{
	public class TakeObject : MonoBehaviour
	{	
		[Header("Positions")]
		[SerializeField] private Vector3 _defoultPositionTakedObject;
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
		
		private bool _isTake;
		private Rigidbody _currentObject;
		private IPhysicObject _physicObject;
		private CollisionDetectionMode _originalMode;
		private RotationAxeses _rotatinAxes;
		
		private MouseLook _cameraRotation;
		private RaycastHit _hit;
		
		public void SetCameraRotationScripts(MouseLook cameraRotation)
		{
			_cameraRotation = cameraRotation;
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
			_isTake = false;
			_currentObject.collisionDetectionMode = _originalMode;
			_currentObject = null;
			_transformTakedObject.position = _defoultPositionTakedObject;
			
			_cameraRotation.SetDownCameraRotatin(-_physicObject.MaxDownCameraRotation);
		}
		
		private void TakeOn()
		{
			_currentObject = CheckIsObjectRigidbody();
				
			_isTake = _currentObject != null;
			
			if (_isTake)
			{
				_originalMode = _currentObject.collisionDetectionMode;
				_currentObject.collisionDetectionMode = CollisionDetectionMode.Continuous;

				 _cameraRotation.SetDownCameraRotatin(_physicObject.MaxDownCameraRotation);
			}
		}
		
		private Rigidbody CheckIsObjectRigidbody()
		{
			var mouseVector = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(mouseVector, out _hit, _maxDistancyToTake))
			{
				_physicObject = _hit.collider.gameObject.GetComponent(typeof(IPhysicObject)) as IPhysicObject;
				
				if (_physicObject == null)
				{ 
					return null;
				}
				
				_transformTakedObject.localPosition = _physicObject.PositionTakedObject;
				return _hit.rigidbody;
			}
			
			return null;
		}
		
		private void AddForceAfterTakeOff()
		{
			_currentObject.AddForce((_transformTakedObject.position - _currentObject.position) * _forseAfterTakeOff, ForceMode.Impulse);
		}
		
		private void UpdateObjectPosition()
		{
			if (Mouse.Stay(MouseButtons.RightButton) && _isTake) 
			{
				_currentObject.MovePosition( Vector3.Lerp(_currentObject.position, _transformTakedObject.position, _speedTake * Time.fixedDeltaTime - (_currentObject.mass * 0.02f) ));
				_currentObject.velocity = Vector3.zero;
				
				if (CheckDistancyToObject()) 
				{
					TakeOff();
				}
			}
		}
		
		private bool CheckDistancyToObject()
		{
			var distancy = Mathf.Sqrt( Mathf.Pow(_currentObject.position.x - _transformTakedObject.position.x, 2) +  Mathf.Pow(_currentObject.position.y - _transformTakedObject.position.y, 2) +  Mathf.Pow(_currentObject.position.z - _transformTakedObject.position.z, 2));
			
			return distancy > _maxDistancyToObject;
		}
		
		private void TakeOffYouself()
		{
			_isTake = false;
			_currentObject = null;
			_transformTakedObject.position = _defoultPositionTakedObject;
			
			_cameraRotation.SetDownCameraRotatin(-_physicObject.MaxDownCameraRotation);
		}
		
		private void CheckObjectThrowYourself()
		{
			if (_isTake && _physicObject.TakeOff)
			{
				TakeOffYouself();
			}
		}
			
		private void Update()
		{
			CheckObjectThrowYourself();
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
			_currentObject.AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * _forceThrowAway, ForceMode.Impulse);
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
			}	
		}
		
		private void RotationObject()
		{
			if (Input.GetAxis("Mouse ScrollWheel") != 0 && _isTake)
			{
				if (_rotatinAxes == RotationAxeses.X)
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0)
					{
						_currentObject.AddTorque(Vector3.right * _speedRotation, ForceMode.Force);
					}
					else
					{
						_currentObject.AddTorque(Vector3.left * _speedRotation, ForceMode.Force);
					}
					
				}
				else if(_rotatinAxes == RotationAxeses.Y)
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0)
					{
						_currentObject.AddTorque(Vector3.up * _speedRotation, ForceMode.Force);
					}
					else
					{
						_currentObject.AddTorque(Vector3.down * _speedRotation, ForceMode.Force);
					}
				}
				else 
				{
					if (Input.GetAxis("Mouse ScrollWheel") > 0)
					{
						_currentObject.AddTorque(Vector3.forward * _speedRotation, ForceMode.Force);
					}
					else
					{
						_currentObject.AddTorque(Vector3.back * _speedRotation, ForceMode.Force);
					}
				}
			}
			
			if (Input.GetKeyDown("f1"))
			{
				_currentObject.angularVelocity = Vector3.zero;
			}
		}
		
		private enum RotationAxeses
		{
			X, Y, Z
		}
	}
}

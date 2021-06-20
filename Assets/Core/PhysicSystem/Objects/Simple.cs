using UnityEngine;
using Core.Camera.Movement;
using Core.Player.Movement.Data;
using Core.Player.Characteristics;

namespace Core.PhysicSystem.Objects
{	
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("PhysicObjects/Simple")]
	public class Simple : MonoBehaviour, IPhysicObject
	{
		[SerializeField] private EditCenterOfMass _editCentreOfMass;
		[Space]
		[SerializeField] private MovementData _movementData;
		[Space]
		[SerializeField] private TakeData _takeData;
		[Space]
		[SerializeField] private ForceThrow _forceThrow;
		[Space]
		[SerializeField] private CameraRestrictions _cameraRestrictions;
		
		public void Take()
		{
			_movementData.SaveOriginalValue();
			_movementData.SetTakedMassObject();
			ApplyCameraRestrictions();
		}
		
		public void ChangeCentreOfMass(Vector3 targeCentreOfMass)
		{
			if (_movementData.EditCentreOfMass) 
			{
				_editCentreOfMass.ApplyCentreOfMass(targeCentreOfMass);
			}
		}
		
		public void ThrowAway(Vector3 vector)
		{
			if (_movementData.EditCentreOfMass) 
			{
				_editCentreOfMass.ResetCentreOfMass();
			}
			
			_movementData.ResetTakedMassObject();
			
			var directionForce = vector - _movementData.Rigidbody.position;
			
			_movementData.Rigidbody.AddForce(directionForce * _forceThrow.ThrowAway, ForceMode.Impulse);
			_movementData.Rigidbody.AddTorque((_forceThrow.GetRandomVectorTorque(_forceThrow.MaxVectorTorque, directionForce) * _forceThrow.ForseTorqueAfterThrowAway), ForceMode.Impulse);
			
			CancelCameraRestrictions();
		}
		
		public void ThrowHard(Vector3 vector)
		{
			ThrowAway(vector);

            _movementData.Rigidbody.AddForce(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition).direction * _forceThrow.ThrowAway, ForceMode.Impulse);
		}
		
		public void Move()
		{
			_movementData.Rigidbody.MovePosition(_movementData.Direction);
					
			_movementData.Rigidbody.velocity = Vector3.zero; // Fix free Rigidbody move
		}
		
		public void UpdateDirection(Vector3 targetPosition, float playerMoveSpeed)
		{
			var speed = ((Mathf.Clamp(0.1f, 100, playerMoveSpeed) * Time.fixedDeltaTime) / _movementData.CurrentSlowingMove);

			_movementData.Direction = Vector3.Lerp( _movementData.Rigidbody.position, targetPosition, speed);
		}
		
		public bool CheckObjectIsActiveForInteraction(Transform player)
		{
			var distancy = Vector3.Distance(player.position, _movementData.Rigidbody.position);
					
			return distancy < _takeData.MaxDistancyToObject;
		}
		
		public Vector3 GetObjectPosition()
		{
			return _movementData.Rigidbody.position;
		}

        public void UpdateEffectsWhenMove(EndurancePlayer enduracne) { }

		private  void ApplyCameraRestrictions()
		{
			MouseLook.AddOffsetValueRatation(_cameraRestrictions.ClampAngle);
		}
		
		private void CancelCameraRestrictions()
		{
			MouseLook.SubtractOffsetValueRatation(_cameraRestrictions.ClampAngle);
		}	
		
		private void OnCollisionEnter()
		{
			_movementData.ApplyCollisionSlowingMove();
		}
		
		private void OnCollisionExit()
		{
			_movementData.ResetSlowingMove();
		}
	}

}

using UnityEngine;
using Core.Camera.Movement;
using Core.Player.Characteristics;
using Core.Player.Movement.Data;

namespace Core.PhysicSystem.Objects
{	
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("PhysicObjects/Complex")]
	public class Complex : MonoBehaviour, IPhysicObject
	{
		[SerializeField] private EditCenterOfMass _editCentreOfMass;
		[Space]
		[SerializeField] private DifficultMovementData _difficultMovementData;
		[Space]
		[SerializeField] private TakeData _takeData;
		[Space]
		[SerializeField] private ForceThrow _forceThrow;
		[Space]
		[SerializeField] private CameraRestrictions _cameraRestrictions;

        private bool _isCancelCameraRestrictions;
		
		public virtual void Take()
		{
			_difficultMovementData.SaveOriginalValue();
			
			_difficultMovementData.StandartMovement.SetTakedMassObject();
			_difficultMovementData.ApplyCollisionDetectionMode();
			_difficultMovementData.ApplyInterpolation();
			ApplyCameraRestrictions();
		}
		
		public void ChangeCentreOfMass(Vector3 targeCentreOfMass)
		{
			if (_difficultMovementData.StandartMovement.EditCentreOfMass) 
			{
				_editCentreOfMass.ApplyCentreOfMass(targeCentreOfMass);
			}
		}
		
		public virtual void ThrowAway(Vector3 vector)
		{
			if (_difficultMovementData.StandartMovement.EditCentreOfMass)
			{
				_editCentreOfMass.ResetCentreOfMass();
			}
			
			_difficultMovementData.StandartMovement.ResetTakedMassObject();
			
			var directionForce = vector - _difficultMovementData.StandartMovement.Rigidbody.position;
			
			_difficultMovementData.StandartMovement.Rigidbody.AddForce(directionForce * _forceThrow.ThrowAway, ForceMode.Impulse);
			_difficultMovementData.StandartMovement.Rigidbody.AddTorque((_forceThrow.GetRandomVectorTorque(_forceThrow.MaxVectorTorque, directionForce) * _forceThrow.ForseTorqueAfterThrowAway), ForceMode.Impulse);

            _difficultMovementData.ResetCollisionDetectionMode();
            _difficultMovementData.ResetInterpolation();
            CancelCameraRestrictions();
		}
		
		public void ThrowHard(Vector3 vector)
		{
			ThrowAway(vector);

            _difficultMovementData.StandartMovement.Rigidbody.AddForce(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition).direction * _forceThrow.ThrowAway, ForceMode.Impulse);
		}
		
		public void Move()
		{
			if (_difficultMovementData.BlockFreeRotation)
			{
				_difficultMovementData.StandartMovement.Rigidbody.rotation = _difficultMovementData.StartRotationVector;
			}
			
			if (_difficultMovementData.BlockMoveY)
			{
				_difficultMovementData.StandartMovement.Direction = _difficultMovementData.GetBlockMoveVectorForY(_difficultMovementData.StandartMovement.Direction);
			}
			
			_difficultMovementData.StandartMovement.Rigidbody.MovePosition(_difficultMovementData.StandartMovement.Direction);
			
			if (!_difficultMovementData.FreeVelocity)
			{
				_difficultMovementData.StandartMovement.Rigidbody.velocity = Vector3.zero;
			}			
		}
		
		public void UpdateDirection(Vector3 targetPosition, float playerMoveSpeed)
		{
			var speed = ((Mathf.Clamp(0.1f, 100, playerMoveSpeed) * Time.fixedDeltaTime) / _difficultMovementData.StandartMovement.CurrentSlowingMove);

			_difficultMovementData.StandartMovement.Direction = Vector3.Lerp( _difficultMovementData.StandartMovement.Rigidbody.position, targetPosition, speed);
		}
		
		public bool CheckObjectIsActiveForInteraction(Transform player)
		{
			var distancy = Vector3.Distance(player.position, _difficultMovementData.StandartMovement.Rigidbody.position);
					
			return distancy < _takeData.MaxDistancyToObject;
		}
		
		public Vector3 GetObjectPosition()
		{
			return _difficultMovementData.StandartMovement.Rigidbody.position;
		}

        public virtual void UpdateEffectsWhenMove(EndurancePlayer enduracne) { }

		protected void ApplyCameraRestrictions()
		{
            if(!_isCancelCameraRestrictions)
            {
			    MouseLook.AddOffsetValueRatation(_cameraRestrictions.ClampAngle);
                _isCancelCameraRestrictions = true;
            }
		}
		
		protected void CancelCameraRestrictions()
		{
            if (_isCancelCameraRestrictions)
            {
                MouseLook.SubtractOffsetValueRatation(_cameraRestrictions.ClampAngle);
                _isCancelCameraRestrictions = false;
            }
		}

        protected void ApplyCollisionSlowingMove()
        {
            _difficultMovementData.StandartMovement.ApplyCollisionSlowingMove();
        }

        protected void ResetCollisionSlowingMove()
        {
            _difficultMovementData.StandartMovement.ResetSlowingMove();
        }

		private void OnCollisionEnter()
		{
            ApplyCollisionSlowingMove();
		}
		
		private void OnCollisionExit()
		{
            ResetCollisionSlowingMove();
		}
	}

}

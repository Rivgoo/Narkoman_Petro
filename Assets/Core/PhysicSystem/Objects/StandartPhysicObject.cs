using UnityEngine;
using Core.Player.Characteristics;
using Core.Player.Physic;
using Core.PhysicSystem.Effects;
using Core.PhysicSystem.Objects.Data;

namespace Core.PhysicSystem.Objects
{
    [AddComponentMenu("PhysicObjects/StandartPhysicObject")]
    [RequireComponent(typeof(PhysicalInteractionWithPlayer), typeof(Rigidbody), typeof(EffectsComponentGroup))]
	public class StandartPhysicObject :  IPhysicObject
	{
        [SerializeField] private EffectsComponentGroup _effects;

		[Space]
		[SerializeField] private MovementData _movementData;
		[SerializeField] private TakeData _takeData;
		[SerializeField] private Forces _forceThrow;
		[SerializeField] private CameraRestrictions _cameraRestrictions;

        private Vector3 _prevosiunDirection;

        public override void Take()
		{
            _movementData.InitData();

			_movementData.SaveOriginalValue();
			_movementData.SetTakedMassObject();
            _movementData.ApplyCollisionSettings();
            _cameraRestrictions.ApplyRestrictions();
		}

        public override void PutObject(Vector3 vector)
		{
            if(_effects.EditCentreOfMass != null)
            {
               _effects.EditCentreOfMass.ResetCentreOfMass();
            }
			
			_movementData.ResetTakedMassObject();
			
			var directionForce = vector - _movementData.Rigidbody.position;
			
			_movementData.Rigidbody.AddForce(directionForce * _forceThrow.Put, ForceMode.Impulse);
			_movementData.Rigidbody.AddTorque((_forceThrow.GetRandomVectorTorque(_forceThrow.MaxVectorTorque, directionForce) * _forceThrow.ForseTorque), ForceMode.Impulse);

            _movementData.ResetCollisionDetectionMode();
            _movementData.ResetInterpolation();
            _cameraRestrictions.CancelRestrictions();
		}

        public override void ThrowObject(Vector3 vector)
		{
			PutObject(vector);

            _movementData.Rigidbody.AddForce(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition).direction * _forceThrow.Put, ForceMode.Impulse);
		}

        public override void Move(Vector3 targetPosition, float playerMoveSpeed)
		{
           var speedMove = Mathf.Clamp(playerMoveSpeed * _movementData.PlayerSpeedRatio, 1, 10) / _movementData.CurrentSlowingMove;

            _movementData.Direction = Vector3.Lerp(_movementData.Rigidbody.position, targetPosition, speedMove * Time.fixedDeltaTime);

            _movementData.ApplyBlockMovement();

            if(_effects.EnduranceSubstruct != null && _movementData.Direction != _prevosiunDirection)
            {
                _effects.EnduranceSubstruct.Substruct();
            }

            _prevosiunDirection = _movementData.Direction;

			_movementData.Rigidbody.MovePosition(_movementData.Direction);			
		}

        public override bool IsCanTakeObject(Transform player)
		{
            var distancy = Vector3.Distance(player.position, _movementData.PointCollision.position);
					
			return distancy < _takeData.MaxDistancyToObject;
		}

        public override bool IsCanKeepingObject(Transform player)
        {
            var distancy = Vector3.Distance(player.position, _movementData.PointCollision.position);

            return distancy < _takeData.MaxDistancyKeepingObject;
        }

        public override Vector3 GetObjectPosition()
		{
			return _movementData.Rigidbody.position;
		}

        public override void SetPointCollision(Vector3 pointCollision)
        {
            _movementData.PointCollision.position = pointCollision;
        }

        public override EffectsComponentGroup GetEffects()
        {
            return _effects;
        }

		private void OnCollisionEnter()
		{
            _movementData.ApplyCollisionSlowingMove();
		}
		
		private void OnCollisionExit()
		{
            _movementData.ResetSlowingMove();
		}

        private void Start()
        {
            _movementData.CurrentSlowingMove = _movementData.SlowingMove;

            if(_effects.DestroyerObject != null)
            {
                _effects.DestroyerObject.DestroyTheObject += _cameraRestrictions.CancelRestrictions;
            }
        }
	}
}

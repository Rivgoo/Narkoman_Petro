using UnityEngine;
using Core.Player.Movement;
using Core.PhysicSystem.Objects;
using Core.Player.Movement.Data;
using Core.Player.Characteristics;

namespace Core.PhysicSystem
{
	public class ObjectMover : MonoBehaviour
	{
		[SerializeField] 
        private Transform _targetObjectPosition;

		[SerializeField] 
        private TakerObject _taker;

		[SerializeField] 
        private PlayerMovement _playerMoveData;

        [SerializeField]
        private EndurancePlayer _endurance;

		private IPhysicObject _physicObject;
		
		private void Take(IPhysicObject physicObject)
		{
			_physicObject = physicObject; 
			_targetObjectPosition.position = physicObject.GetObjectPosition();
		}

        private void UpdatePointCollisionWithObject(Vector3 pointCollisionWithObject)
        {
            _physicObject.ChangeCentreOfMass(pointCollisionWithObject);
        }
		
		private void ThrowHard()
		{
			_physicObject.ThrowHard(_targetObjectPosition.position);
		}
		
		private void ThowAway()
		{
			_physicObject.ThrowAway(_targetObjectPosition.position);
		}
		
		private void Move()
		{
			if (_physicObject != null)
			{			
				if (TakerObject.IsTaking)
				{
					_physicObject.UpdateDirection(_targetObjectPosition.position, _playerMoveData.SpeedsValue.Current);
					_physicObject.Move();
                    _physicObject.UpdateEffectsWhenMove(_endurance);
				}
			}
		}
		
		private void FixedUpdate()
		{
			Move();
		}
		
		private void Start()
		{
			SubscribeEventThrow();
		}
		
		private void SubscribeEventThrow()
		{
			_taker.ThrewAway += ThowAway;
			_taker.ThrewHard += ThrowHard;
			_taker.Taked += Take;
			_taker.ResettingTargetPhysicObject += ResetTargetPhysicObject;
            _taker.UpdatedpointCollisionWithObject += UpdatePointCollisionWithObject;
		}
		
		private void ResetTargetPhysicObject()
		{
			_physicObject = null;
		}
	}
}
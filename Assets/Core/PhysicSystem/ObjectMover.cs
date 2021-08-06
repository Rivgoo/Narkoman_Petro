using UnityEngine;
using Core.PhysicSystem.Objects;
using Core.Player.Movement.Data;

namespace Core.PhysicSystem
{
	public class ObjectMover : MonoBehaviour
	{
        [SerializeField]
        private Vector3 _normalPosition;

        [Space]
		[SerializeField] 
        private Transform _targetObjectPosition;

		[SerializeField] 
        private TakerObject _taker;

		[SerializeField] 
        private PlayerMovement _playerMoveData;

		private IPhysicObject _physicObject;
		
		private void Take(IPhysicObject physicObject)
		{
			_physicObject = physicObject; 
			_targetObjectPosition.position = physicObject.GetObjectPosition();
		}
		
		private void ThrowObject()
		{
			_physicObject.ThrowObject(_targetObjectPosition.position);
		}
		
		private void PutObject()
		{
			_physicObject.PutObject(_targetObjectPosition.position);
		}
		
		private void Move()
		{
            if(_physicObject != null && TakerObject.IsKeeping)
			{			
               _physicObject.Move(_targetObjectPosition.position, _playerMoveData.SpeedsValue.Current);
			}
		}

        private void NormalizePosition()
        {
            _targetObjectPosition.localPosition = _normalPosition;
        }
		
		private void FixedUpdate()
		{
			Move();
		}
		
		private void Start()
		{
			SubscribeTakerEvent();
		}
		
		private void SubscribeTakerEvent()
		{
			_taker.PutObject += PutObject;
			_taker.ThrewObject += ThrowObject;
			_taker.Taked += Take;
			_taker.ResettingTargetPhysicObject += ResetTargetPhysicObject;
            _taker.NormalizePositionToObject += NormalizePosition;
		}
		
		private void ResetTargetPhysicObject()
		{
			_physicObject = null;
		}
	}
}
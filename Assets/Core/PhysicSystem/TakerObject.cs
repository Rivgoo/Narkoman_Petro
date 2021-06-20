using UnityEngine;
using Mouse = PlayerInput.KeysInput.CheckMouseButton; 
using PlayerInput;
using System;
using Core.Player.Movement;
using Core.PhysicSystem.Objects;
using Core.Player.Movement.Data;

namespace Core.PhysicSystem
{
	public class TakerObject : MonoBehaviour
	{
		public static bool IsTaking { get; private set; }
		
		public event Action ThrewAway;
		public event Action ThrewHard;
		public event Action<IPhysicObject> Taked;
        public event Action<Vector3> UpdatedpointCollisionWithObject;
		public event Action ResettingTargetPhysicObject;
		
		[SerializeField] private float _maxTakerDistancy;
		[Space]
		[SerializeField] private Core.Player.Characteristics.EndurancePlayer _endurancy;	
		[SerializeField] private PlayerMovement _playerMoveData;
		[SerializeField] private Transform _player;
		
		private IPhysicObject _physicObject;
		
		private void Update()
		{
			if (IsTaking) 
			{
				CheckThrow();
			}
			else
			{
				CheckTake();
			}
		}
			
		private bool TryTake(out IPhysicObject physicObject, out Vector3 pointCollisionWithObject)
		{	
			var mouseVector = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			pointCollisionWithObject = Vector3.zero;
			
			if (Physics.Raycast(mouseVector, out hit, _maxTakerDistancy))
			{
				physicObject = hit.collider.gameObject.GetComponent(typeof(IPhysicObject)) as IPhysicObject;
				
				if ( physicObject != null && physicObject.CheckObjectIsActiveForInteraction(transform))
				{
					pointCollisionWithObject = hit.point;
					return true;
				}
			}
			
			physicObject = null;
			
			return false;
		}
		
		private void CheckTake()
		{
			if (Mouse.Down(MouseButtons.RightButton) && _endurancy.CheckIfCanTake())
			{
				Vector3 pointCollisionWithObject;
				
				IsTaking = TryTake(out _physicObject, out pointCollisionWithObject);
				
				if (IsTaking)
				{
					_physicObject.Take();
				
					Taked(_physicObject);
                    UpdatedpointCollisionWithObject(pointCollisionWithObject);
				}
			}
		}
		
		private void CheckThrow()
		{
			if (_physicObject != null)
			{
				if (Mouse.Down(MouseButtons.LeftButton))
				{
					//Threw Hard = Threw Hard + Threw Away
					IsTaking = false;
					ThrewHard();
					ResettingTargetPhysicObject();
						
				}
				else if(!_physicObject.CheckObjectIsActiveForInteraction(_player) || Mouse.Up(MouseButtons.RightButton) || !_endurancy.CheckIfCanKeep())
				{
					IsTaking = false;
					ThrewAway();
					ResettingTargetPhysicObject();
				}
			}
			else
			{
				IsTaking = false;
				ResettingTargetPhysicObject();
			}
		}
	}
}
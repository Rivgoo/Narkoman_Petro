using System;
using UnityEngine; 
using Core.InputSystem;
using Core.Player.Movement;
using Core.PhysicSystem.Objects;
using Core.Player.Movement.Data;
using Core.Player.Characteristics;

namespace Core.PhysicSystem
{
	public class TakerObject : MonoBehaviour
	{
		public static bool IsKeeping { get; private set; }
		
		public event Action PutObject;
		public event Action ThrewObject;
		public event Action<IPhysicObject> Taked;
		public event Action ResettingTargetPhysicObject;
        public event Action NormalizePositionToObject;

        [SerializeField]
        private MouseInput _mouseInput;

        [Space]
        [SerializeField] 
        private LayerMask _ignorLayers;

        [Space]
		[SerializeField] 
        private float _maxTakerDistancy;

		[Space]
		[SerializeField]
        private EndurancePlayer _endurancy;	

		[SerializeField] 
        private PlayerMovement _playerMoveData;

		[SerializeField]
        private Transform _player;

        [Space]
        [SerializeField] [ReadOnly]
		private IPhysicObject _physicObject;

        private Vector3 _pointCollisionWithObject;
		
		private void Update()
		{
			if (IsKeeping) 
			{
				CheckInput();
			}
			else
			{
				CheckTake();
			}
		}
			
		private bool TryTake(out IPhysicObject physicObject)
		{	
			var mouseVector = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;

            if(Physics.Raycast(mouseVector, out hit, _maxTakerDistancy, _ignorLayers))
			{
				physicObject = hit.collider.gameObject.GetComponent(typeof(IPhysicObject)) as IPhysicObject;
				
				if (physicObject != null)
				{
                    _pointCollisionWithObject = hit.point;
                    physicObject.SetPointCollision(hit.point);

                    if(physicObject.IsCanTakeObject(_player))
                    {
                        ApplyEffects(physicObject);

					    return true;
                    }
				}
			}
			
			physicObject = null;
			
			return false;
		}

        private void ApplyEffects(IPhysicObject physicObject)
        {
            var effectsGroup = physicObject.GetEffects();

            if(effectsGroup.EnduranceSubstruct != null)
            {
                effectsGroup.EnduranceSubstruct.Endurance = _endurancy;
            }

            if(effectsGroup.EditCentreOfMass)
            {
                effectsGroup.EditCentreOfMass.TargetCentreOfPosition = _pointCollisionWithObject;
            }
        }

		private void CheckTake()
		{
			if (_mouseInput.Down(MouseButtons.RightButton) && _endurancy.CheckIfCanTake())
			{
                if(TryTake(out _physicObject))
				{
                    IsKeeping = true;

					_physicObject.Take();
				
					Taked(_physicObject);
				}
			}
		}
		
		private void CheckInput()
		{
			if (_physicObject != null)
			{
                if(_mouseInput.Down(MouseButtons.ScrollLock))
                {
                    NormalizePositionToObject.Invoke();
                }

				if (_mouseInput.Down(MouseButtons.LeftButton))
				{
					ThrewObject();
                    ResetTaker();
						
				}
				else if(!_physicObject.IsCanKeepingObject(_player) || _mouseInput.Up(MouseButtons.RightButton) || !_endurancy.CheckIfCanKeep())
				{
                    PutObject();
					ResetTaker();
				}
			}
			else
			{
                ResetTaker();
			}
		}

        private void ResetTaker()
        {
            IsKeeping = false;
            _physicObject = null;
            ResettingTargetPhysicObject();
        }
	}
}
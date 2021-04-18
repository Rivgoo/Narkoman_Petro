using UnityEngine;
using PlayerData;

namespace Objects
{
	public class PhysicObjectDefoult : MonoBehaviour, IPhysicObject 
	{
		[Header("Settings")] 
		[SerializeField] protected PhysicObjectData _dataObject;
		
		public PhysicObjectData DataObject { get { return _dataObject; } set { _dataObject = value; }}
		
		public virtual void Move(Vector3 direction)
		{
			DataObject.Rigidbody.MovePosition(direction);
					
			DataObject.Rigidbody.velocity = Vector3.zero; // Fix free Rigidbody move
		}
		
		public virtual Vector3 GetObjectPosition()
		{
			return _dataObject.Rigidbody.position;
		}
		
		public virtual void TakeOff()
		{
			_dataObject.Rigidbody.mass = _dataObject.ObjectMovement.OriginalMassaObject;
			
			//Reset Collision Mode and Interpolation
			_dataObject.Rigidbody.collisionDetectionMode = _dataObject.ObjectMovement.OriginalDetectionMode;
			_dataObject.Rigidbody.interpolation = _dataObject.ObjectMovement.OriginalInterpolationMode;
		}
		
		public virtual void TakeOn()
		{
			// Set Collision Move
			if (_dataObject.ObjectMovement.UseContinuousDynamic)
			{
				_dataObject.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
			
			//Set Interpolation Mode
			if (_dataObject.ObjectMovement.UseInterpolate)
			{
				_dataObject.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
				
			// Set mass
			_dataObject.Rigidbody.mass = _dataObject.ObjectMovement.TakedMassaObject;
		}
		
		private void Start() 
		{
			_dataObject.ObjectMovement.OriginalMassaObject = _dataObject.Rigidbody.mass;
			_dataObject.ObjectMovement.OriginalInterpolationMode = _dataObject.Rigidbody.interpolation;
			_dataObject.ObjectMovement.OriginalDetectionMode = _dataObject.Rigidbody.collisionDetectionMode;
		}
	}
}

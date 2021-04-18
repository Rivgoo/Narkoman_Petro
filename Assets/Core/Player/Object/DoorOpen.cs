using UnityEngine;

namespace Objects
{
	public class DoorOpen : PhysicObjectDefoult
	{
		[Space]
		[SerializeField] private Transform _centreDoor;

		[SerializeField] private AngleSing _currentSign;
		[SerializeField] private Vector3 _eulerAngleVelocity;
		
		public override void Move(Vector3 direction)
		{
			Quaternion deltaRotation = Quaternion.Euler((_eulerAngleVelocity * Time.fixedDeltaTime) * (int)_currentSign);
			
			DataObject.Rigidbody.MoveRotation(DataObject.Rigidbody.rotation * deltaRotation);
		}
		
		public override void TakeOff()
		{
			SetCurrentSign();
		} 
		
		public override Vector3 GetObjectPosition()
		{
			return _centreDoor.position;
		} 
		
		private void SetCurrentSign()
		{
			_currentSign = _currentSign == AngleSing.Plus ? AngleSing.Minus : AngleSing.Plus;
		} 
		
		private enum AngleSing
		{
			Plus = 1,
			Minus = -1
		}
	}
}

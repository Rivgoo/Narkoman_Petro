using UnityEngine;

namespace Objects
{
	public class DoorRotation : MonoBehaviour
	{
		[SerializeField] private Transform _door;
		[SerializeField] private Transform _rotationPoint;
		
		[SerializeField] private Transform _movePoint;
		[SerializeField] private Rigidbody _moveObject;
		
		[Space]
		[SerializeField] private Vector3 _axesRotation;
		[SerializeField] private Vector3 _vectorRotation;
		
		private void Update()
		{
			SetRotation();
		}
		
		private void SetRotation()
		{
			_door.rotation = Quaternion.Euler(_door.rotation.x,  GetAngle(), _door.rotation.z);
			_movePoint.rotation = _door.rotation;
		}
		
		private float GetAngle()
		{
			return SignedAngle((_movePoint.position - _rotationPoint.position).normalized, _vectorRotation, _axesRotation);
		}
		
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            float unsignedAngle = Vector3.Angle(from, to);
 
            float cross_x = from.y * to.z - from.z * to.y;
            float cross_y = from.z * to.x - from.x * to.z;
            float cross_z = from.x * to.y - from.y * to.x;
            float sign = Mathf.Sign(axis.x * cross_x + axis.y * cross_y + axis.z * cross_z);
            return unsignedAngle * sign;
        }
	}
}
using UnityEngine;
using PlayerData;

namespace Objects
{
	public class PhysicObjectDefoult : MonoBehaviour, IPhysicObject 
	{
		[Header("Rigidbody")]
		[SerializeField] protected Rigidbody _rigidbody;
		
		[Header("Speeds Player When Object Take on (Value % * massa object)")]
		[SerializeField] private PlayerSpeeds _speeds;
		
		[Header("Position Taked Object")]
		[SerializeField] protected Vector3 _positionTakedObject; 
		
		[Header("Max Down Camera Rotation")]
		[SerializeField] protected float _maxDownCameraRotation;
		
		[Header("Max Vector Torque")]
		[SerializeField] protected Vector3 _maxVectorTorque;
		
		public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; }}
		public Vector3 PositionTakedObject { get{ return _positionTakedObject; } }
		public float MaxDownCameraRotation { get { return _maxDownCameraRotation; } }
		public PlayerSpeeds Speeds { get { return _speeds;} }
		public Vector3 MaxVectorTorque { get { return _maxVectorTorque; } }
		public bool IsTake { get; set; }
	}
}

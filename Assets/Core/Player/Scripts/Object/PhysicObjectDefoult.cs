using UnityEngine;

public class PhysicObjectDefoult : MonoBehaviour, IPhysicObject
{
	[SerializeField] protected Vector3 _positionTakedObject; 
	[SerializeField] protected float _maxDownCameraRotation;
	[SerializeField] protected Rigidbody _rigidbody;
	[SerializeField] protected bool _takeOff;
		
	public Vector3 PositionTakedObject { get{ return _positionTakedObject; } }
	public float MaxDownCameraRotation { get { return _maxDownCameraRotation; } }
	public bool TakeOff { get { return _takeOff; } }
}


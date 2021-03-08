using UnityEngine;

public interface IPhysicObject
{
	Vector3 PositionTakedObject { get; }
	float MaxDownCameraRotation { get; }
	
	bool TakeOff { get; }
}

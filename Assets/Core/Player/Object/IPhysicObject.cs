using UnityEngine;
using PlayerData;

namespace Objects
{
	public interface IPhysicObject
	{
		Vector3 PositionTakedObject { get; }
		float MaxDownCameraRotation { get; }
		Rigidbody Rigidbody { get; set; }
		PlayerSpeeds Speeds { get; } 
		Vector3 MaxVectorTorque { get; }
		bool IsTake { get; set; }
	}
}
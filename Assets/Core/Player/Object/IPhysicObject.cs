using UnityEngine;
using PlayerData;

namespace Objects
{
	public interface IPhysicObject
	{
		PhysicObjectData DataObject { get; set; }
		
		void Move(Vector3 direction);
		Vector3 GetObjectPosition();
		void TakeOff();
		void TakeOn();
	}
	
	[System.Serializable]
	public struct PhysicObjectData
	{
		public Rigidbody Rigidbody;

		[Range(0.1f, 10)]public float MaxDistancyToTake;

		[Range(0.1f, 3)] public float MaxDistancyToObject;
		
		[Space]
		public ObjectSettingsMovement ObjectMovement;
		
		[Space]
		public ForceTakeOff ForceTakeOff;
		
		[Space]
		public PhysicObjectSettingsForPlayer SettingsForPlayer;
	}
	
	[System.Serializable]
	public struct ForceTakeOff
	{
		public Vector3 MaxVectorTorque;
		
		[Range(1, 100)] public float ForceThrowAway;
		
		[Range(1, 100)] public float ForseAfterTakeOff;
		[Range(1, 100)] public float ForseTorqueAfterTakeOff;
	}
	
	[System.Serializable]
	public struct PhysicObjectSettingsForPlayer
	{
		[Range(0, 1)]public float DropRateEndurancy;
		
		public Vector2Angle ClampAngle;
		
		public PlayerSpeeds Speeds;
	}
	
	[System.Serializable]
	public struct ObjectSettingsMovement
	{
		public bool BlockMoveY;
		public bool BlockFreeRotation;
		
		public bool UseInterpolate;
		public bool UseContinuousDynamic;
		
		[Space]
		[Range(1, 1000)]public float SlowingMove;
		
		public float TakedMassaObject;
		
		[Space]
		[ReadOnly] public float OriginalMassaObject;
		[ReadOnly] public RigidbodyInterpolation OriginalInterpolationMode;
		[ReadOnly] public CollisionDetectionMode OriginalDetectionMode;
	}
}
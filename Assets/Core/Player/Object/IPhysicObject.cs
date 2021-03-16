using UnityEngine;
using PlayerData;

namespace Objects
{
	public interface IPhysicObject
	{
		DefoultPhysicObjectData DataObject { get; set; }
	}
	
	[System.Serializable]
	public struct DefoultPhysicObjectData
	{
		[Header("Rigidbody")]
		public Rigidbody Rigidbody;
		
		[Header("Position Taked Object")]
		public Vector3 PositionTakedObject;
		
		[Header("Type Take Positio")]
		public TypeTakePosition TypeTakePosition;
		
		[Header("Moz Distancy To take")]
		[Range(0.1f, 10)]public float MaxDistancyToTake;
		
		[Header("Max Distancy to Object")]
		[Range(0.1f, 3)] public float MaxDistancyToObject;
		
		[Header("Block Move Y")]
		public bool BlockMoveY;
		
		[Header("Block Free Rotation")]
		public bool BlockFreeRotation;
		
		[Header("Block Player Rotation")]
		public bool BlockPlayerRotation;
		
		[Header("Max Camera Rotation (X = Down| Y = Up)")]
		public Vector2 MaxCameraRoation;
		
		[Header("Speeds Player When Object Take on (Value % * massa object)")]
		public PlayerSpeeds Speeds;
		
		[Header("Max Vector Torque")]
		public Vector3 MaxVectorTorque;
		
		[Header("Force Throw Object")]
		[Range(1, 100)] public float ForceThrowAway;
		
		[Header("Speed Force After Take Off")]
		[Range(1, 100)] public float ForseAfterTakeOff;
		[Range(1, 100)] public float ForseTorqueAfterTakeOff;
		
		[Header("Slowing Move")]
		[Range(1, 1000)]public float SlowingMove;
		
		[Header("Drop Rate Endurancy")]
		[Range(0, 1)]public float DropRateEndurancy;
	}
	
	public enum TypeTakePosition
	{
		Object, Player
	}
}
using UnityEngine;
using Player;

namespace PlayerData
{
	[System.Serializable]
	[SerializeField]
	public class MovementPlayerData 
	{
		public CharacterController CharacterController;
		
		public CrouchPlayer Crouch;
		public PlayerSpeeds Speeds;
		public PlayerSpeedsSettings SpeedsSettings;
		public GravityPlayer Gravity;
		public Step Step;
		public PhysicObjects Physic;
		
		
		[Header("Info Data")]
		public BlockMovementPlayer BlockMovement;
		public StateMovementPlayer State;
		public Move Move;
		
		public MovementPlayerData()
		{
			Step.StepCycle = 0f;
            Step.NextStep = Step.StepCycle / 2f;
            State.IsJumping = false;
		}

	}
	
	public static class CalculationNumber
	{
		public static float GetNumber(float originalValue, float targetPercent)
		{
			return (originalValue * targetPercent) / 100;
		}
	}
	
	[System.Serializable]
    [SerializeField]
    public struct PlayerSpeeds
    { 	
        public float Walk; 
        public float Run; 
        public float Jump; 
        public float Crouch; 
        
        [Header("Info")]
        [ReadOnly] public float Current;
    }
    
    [System.Serializable]
    [SerializeField]
    public struct PlayerSpeedsSettings
    {
        public float SpeedTransitionBetweenSpeeds;
    }
    
	[System.Serializable]
	[SerializeField]
	public struct Step
	{
        public float StepInterval;
        
        [Range(0, 3f)] public float RunStepLenghten;
        [Range(0, 3f)] public float WalkStepLenghten;
        [Range(0, 3f)] public float CrouchStepLenghten;
        
        [Header("Info")]
        [ReadOnly] public float StepCycle;
       	[ReadOnly] public float NextStep;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct PhysicObjects
	{
		[Header("Forces")]
		public float ForceSmallObject;
		public float ForceWalk;
		public float ForceCrouch;
		public float ForceRun;
		
		[Header("Info")]
		[ReadOnly] public float CurrentForce;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct Move
	{
		[ReadOnly] public Vector3 Direction;
		[ReadOnly] public Vector2 Input;
		
		[ReadOnly] public float VerticalAxes;				
		[ReadOnly] public float HorizontalAxes;
		
		[ReadOnly] public CollisionFlags Collision;

		[HideInInspector]
		public RaycastHit HitInfo; 
	}
	
	[System.Serializable]
	[SerializeField]
	public struct CrouchPlayer
	{
		public float CharacterHeight;
		public float CrouchHeight;
		public float SpeedUp;
		public float SpeedDown;
		
		public float CameraHeightDown;
		
        [Header("Raycast")]
        public float DistanceCheckCrouchRaycast;
        
        [Header("Info")]
        [ReadOnly] public bool CheckCrouchRaycast;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct StateMovementPlayer
	{
		[ReadOnly] public bool IsJumping;
		[ReadOnly] public bool IsCrouch;
	    [ReadOnly] public bool IsWalking;
	    [ReadOnly] public bool IsMovePlayer;
	    [ReadOnly] public bool IsRun;
		[ReadOnly] public bool UpCharacter;
		[ReadOnly] public bool PreviouslyGrounded;
		[ReadOnly] public bool IsJump;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct BlockMovementPlayer
	{
		public bool Jump;
		public bool Crouch;
	    public bool MovePlayer;
	    public bool Run;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct GravityPlayer 
	{
		public float GravityForce; 
		[Space]
		public float StickToGroundForce;
		[Space]
		public Vector3 WorldGravity;
		
		[Header("Info")]
		[ReadOnly] public Vector3 OriginalWorldGravity;
	}
}
	


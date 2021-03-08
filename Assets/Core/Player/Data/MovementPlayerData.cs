using UnityEngine;
using Player;

namespace PlayerData
{
	[System.Serializable]
	[SerializeField]
	public class MovementPlayerData 
	{
		[Header("Player Character Controller")]
		public CharacterController CharacterController;
		
		[Header("Squatting Settings")]
		public CrouchPlayer Crouch;
		[Header("Speed Settings")]
		public PlayerSpeeds Speeds;
		[Header("Gravity Settings")]
		public GravityPlayer Gravity;
		[Header("Step Settings")]
		public Step Step;
		
		[Header("Physic Objects Settings")]
		public PhysicObjects Physic;
		
		[Header("Info Data")]
		public StateMovementPlayer State;
		public Move Move;
		
		public MovementPlayerData()
		{
			Step.StepCycle = 0f;
            Step.NextStep = Step.StepCycle / 2f;
            State.IsJumping = false;
		}

	}
	
	[System.Serializable]
    [SerializeField]
    public struct PlayerSpeeds
    {
        public float Walk; 
        public float Run; 
        public float Jump; 
        public float Squatting; 
        
        [Header("Transition Between Speeds")]
        public float SpeedTransition;
        
        [Header("Info")]
        [ReadOnly] public float CurrentSpeed;
        
    }
    
	[System.Serializable]
	[SerializeField]
	public struct Step
	{
        public float StepInterval;
        
        [Range(0, 3f)] public float RunStepLenghten;
        [Range(0, 3f)] public float WalkStepLenghten;
        [Range(0, 3f)] public float SquattingStepLenghten;
        
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
		public float Force;
		
		[Header("Info")]
		[ReadOnly] public CollisionFlags Collision;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct Move
	{
		[ReadOnly] public Vector3 Direction;
		[ReadOnly] public Vector2 Input;
		
		[ReadOnly] public float VerticalAxes;				
		[ReadOnly] public float HorizontalAxes;
		
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
	public struct GravityPlayer
	{
		public float GravityForce; 
		[Space]
		public float StickToGroundForce;
		
	}
}
	


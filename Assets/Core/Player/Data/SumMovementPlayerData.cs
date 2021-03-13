//using UnityEngine;
//using Player;
//
//namespace PlayerData
//{
//	public class SumMovementPlayerData 
//	{
//		private static MovementPlayerData Defoult;
//		private static EditMovementPlayerData Edit;
//		
//		public CharacterController CharacterController;
//		
//		public CrouchPlayerSum Crouch;
//		public PlayerSpeedsSum Speeds;
//		public GravityPlayerSum Gravity;
//		public StepSum Step ;
//		
//		public PhysicObjectsSum Physic;
//		
//		public StateMovementPlayerSum State; 
//		public MoveSum Move;
//		
//		public void Init(MovementPlayerData defoultMovement, EditMovementPlayerData edit)
//		{
//			Defoult = defoultMovement;
//			Edit = edit;
//		}
//		
//		//Defoult. + Edit.;
//		// { get { return ; } set { = value; } }
//		public static class PlayerSpeedsSum
//	    { 	
//			public static float Walk { get { return Defoult.Speeds.Walk + Edit.Speeds.Walk; } set { Defoult.Speeds.Walk = value; } }
//			public static float Run { get { return Defoult.Speeds.Run + Edit.Speeds.Run; } set{ Defoult.Speeds.Run = value; } }
//	        public static float Jump { get { return Defoult.Speeds.Jump + Edit.Speeds.Jump; } set{ Defoult.Speeds.Jump = value; } }
//	        public static float Squatting { get { return Defoult.Speeds.Squatting + Edit.Speeds.Squatting; } set{ Defoult.Speeds.Squatting = value; } }
//	        
//	        public static float SpeedTransition { get { return Defoult.Speeds.SpeedTransition + Edit.Speeds.SpeedTransition; } set{ Defoult.Speeds.SpeedTransition = value; } }
//	        
//	        public static float CurrentSpeed { get { return Defoult.Speeds.CurrentSpeed + Edit.Speeds.CurrentSpeed; } set{ Defoult.Speeds.CurrentSpeed = value; } }
//	        
//	    }
//	    
//		public class StepSum
//		{
//			public float StepInterval { get { return ; } set { = value; } }
//	        
//	        public float RunStepLenghten { get { return ; } set { = value; } }
//	        public float WalkStepLenghten { get { return ; } set { = value; } }
//	        public float CrouchStepLenghten { get { return ; } set { = value; } }
//	        
//	        public float StepCycle { get { return ; } set { = value; } }
//	       	public float NextStep { get { return ; } set { = value; } }
//		}
//		
//		public class PhysicObjectsSum
//		{
//			public float ForceSmallObject;
//			public float Force;
//			
//			public CollisionFlags Collision;
//		}
//		
//		public class MoveSum
//		{
//			public Vector3 Direction;
//			public Vector2 Input;
//			
//			public float VerticalAxes;				
//			public float HorizontalAxes;
//			
//			public RaycastHit HitInfo; 
//		}
//	
//		public class CrouchPlayerSum
//		{
//			public float CharacterHeight;
//			public float CrouchHeight;
//			public float SpeedUp;
//			public float SpeedDown;
//			
//			public float CameraHeightDown;
//			
//	        public float DistanceCheckCrouchRaycast;
//	        
//	        public bool CheckCrouchRaycast;
//		}
//		
//		public class StateMovementPlayerSum
//		{
//			public bool IsJumping;
//			public bool IsCrouch;
//		    public bool IsWalking;
//		    public bool IsMovePlayer;
//		    public bool IsRun;
//			public bool UpCharacter;
//			public bool PreviouslyGrounded;
//			public bool IsJump;
//		}
//		
//		public class GravityPlayerSum
//		{
//			public float GravityForce; 
//			
//			public float StickToGroundForce;
//			
//			public Vector3 WorldGravity;
//			
//			public Vector3 OriginalWorldGravity;
//		}
//	}
//	
//   
//}
//	
//

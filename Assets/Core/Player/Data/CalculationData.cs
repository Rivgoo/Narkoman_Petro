//using UnityEngine;
//using Player;
//
//namespace PlayerData
//{
//	public static class CalculationData
//	{
//		public static float Get(float defoultValue, float editValue)
//		{
//			return defoultValue + editValue;
//		}
//		
//		public static bool Get(bool editValue)
//		{
//			return editValue;
//		}
//		
//		public static void Set(ref float editValue, float setValue)
//		{
//			editValue += setValue;
//		}
//		
//		public static void Set(ref bool editValue, bool setValue)
//		{
//			editValue = setValue;
//		}
//	}
//	
//	public class InitCalculationPlayerData
//	{
//		protected MovementPlayerData _defoult;
//		protected MovementPlayerData _edit;	
//		
//		public virtual void Init(MovementPlayerData defoultMovement, MovementPlayerData edit)
//		{
//			_defoult = defoultMovement;
//			_edit = edit;
//		}
//	}
//	
//	public class CalculationPlayerSpeeds : InitCalculationPlayerData
//	{		
//		public float Walk { get { return CalculationData.Get(_defoult.Speeds.Walk,_edit.Speeds.Walk); } set { CalculationData.Set(ref _edit.Speeds.Walk, value); } }
//		public float Run { get { return CalculationData.Get(_defoult.Speeds.Run, _edit.Speeds.Run); } set{ CalculationData.Set(ref _edit.Speeds.Run, value); } }
//		public float Jump { get { return CalculationData.Get(_defoult.Speeds.Jump, _edit.Speeds.Jump); } set{ CalculationData.Set(ref _edit.Speeds.Jump, value); } }
//		public float Squatting { get { return CalculationData.Get(_defoult.Speeds.Squatting, _edit.Speeds.Squatting); } set{ CalculationData.Set(ref _edit.Speeds.Squatting, value); } }
//		public float SpeedTransition { get { return CalculationData.Get(_defoult.Speeds.SpeedTransition, _edit.Speeds.SpeedTransition); } set{ CalculationData.Set(ref _edit.Speeds.SpeedTransition, value); } }
//		public float CurrentSpeed { get { return CalculationData.Get(_defoult.Speeds.CurrentSpeed, _edit.Speeds.CurrentSpeed); } set{ CalculationData.Set(ref _edit.Speeds.CurrentSpeed, value); } }
//	}  
//	
//	public class CalculationStep : InitCalculationPlayerData
//	{
//        public float StepInterval { get { CalculationData.Get(_defoult.Step.StepInterval,  _edit.Step.StepInterval);  } set { CalculationData.Set(ref _edit.Step.StepInterval, value); } }
// 		public float RunStepLenghten { get { CalculationData.Get(_defoult.Step.RunStepLenghten,  _edit.Step.RunStepLenghten);  } set { CalculationData.Set(ref _edit.Step.RunStepLenghten, value); } }
//        public float WalkStepLenghten { get { CalculationData.Get(_defoult.Step.WalkStepLenghten,  _edit.Step.WalkStepLenghten);  } set { CalculationData.Set(ref _edit.Step.WalkStepLenghten, value); } }
//        public float CrouchStepLenghten { get { CalculationData.Get(_defoult.Step.CrouchStepLenghten,  _edit.Step.CrouchStepLenghten);  } set { CalculationData.Set(ref _edit.Step.CrouchStepLenghten, value); } }
//        
//        public float StepCycle { get { CalculationData.Get(_defoult.Step.StepCycle,  _edit.Step.StepCycle);  } set { CalculationData.Set(ref _edit.Step.StepCycle, value); } }
//       	public float NextStep { get { CalculationData.Get(_defoult.Step.NextStep,  _edit.Step.NextStep);  } set { CalculationData.Set(ref _edit.Step.NextStep, value); } }
//	}
//	
//	public class CalculationPhysicObjects : InitCalculationPlayerData
//	{
//		public float ForceSmallObject { get { CalculationData.Get(_defoult.Physic.ForceSmallObject,  _edit.Physic.Force);  } set { CalculationData.Set(ref _edit.Physic.ForceSmallObject, value); } }
//		public float Force { get { CalculationData.Get(_defoult.Physic.ForceSmallObject,  _edit.Physic.Force);  } set { CalculationData.Set(ref _edit.Physic.Force, value); } }
//		
//		public CollisionFlags Collision { get { return _edit.Physic.Collision;} set { _edit.Physic.Collision = value; }}
//	}
//	
//	public class CalculationMove : InitCalculationPlayerData
//	{
//		[ReadOnly] public Vector3 Direction;
//		[ReadOnly] public Vector2 Input;
//		
//		[ReadOnly] public float VerticalAxes;				
//		[ReadOnly] public float HorizontalAxes;
//		
//		[HideInInspector]
//		public RaycastHit HitInfo; 
//	}
//	
//	public class CalculationCrouchPlayer : InitCalculationPlayerData
//	{
//		public float CharacterHeight;
//		public float CrouchHeight;
//		public float SpeedUp;
//		public float SpeedDown;
//		
//		public float CameraHeightDown;
//		
//        [Header("Raycast")]
//        public float DistanceCheckCrouchRaycast;
//        
//        [Header("Info")]
//        [ReadOnly] public bool CheckCrouchRaycast;
//	}
//	
//	public class CalculationStateMovementPlayer : InitCalculationPlayerData
//	{
//		[ReadOnly] public bool IsJumping;
//		[ReadOnly] public bool IsCrouch;
//	    [ReadOnly] public bool IsWalking;
//	    [ReadOnly] public bool IsMovePlayer;
//	    [ReadOnly] public bool IsRun;
//		[ReadOnly] public bool UpCharacter;
//		[ReadOnly] public bool PreviouslyGrounded;
//		[ReadOnly] public bool IsJump;
//	}
//	
//	public class CalculationGravityPlayer : InitCalculationPlayerData
//	{
//		public float GravityForce; 
//		public float StickToGroundForce;
//		public Vector3 WorldGravity;
//		
//		public Vector3 OriginalWorldGravity;
//	}
//}
//	
//

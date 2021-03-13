//using UnityEngine;
//using Player;
//
//namespace PlayerData
//{
//	public class CurrentMovementPlayerData
//	{		
//		public CharacterController CharacterController;
//		
//		public CalculationCrouchPlayer Crouch = new CalculationCrouchPlayer();
//		public CalculationPlayerSpeeds Speeds = new CalculationPlayerSpeeds();
//		public CalculationGravityPlayer Gravity = new CalculationGravityPlayer();
//		public CalculationStep Step = new CalculationStep();
//		
//		public CalculationPhysicObjects Physic = new CalculationPhysicObjects();
//		
//		public CalculationStateMovementPlayer State = new CalculationStateMovementPlayer();
//		public CalculationMove Move = new CalculationMove();
//		
//		public void Init(MovementPlayerData defoultMovement, MovementPlayerData edit)
//		{
//			Speeds.Init(defoultMovement, edit);
//			Crouch.Init(defoultMovement, edit);
//			Gravity.Init(defoultMovement, edit); 
//			Step.Init(defoultMovement, edit);
//			State.Init(defoultMovement, edit);
//			Move.Init(defoultMovement, edit);
//		}
//		
//		//Defoult. + Edit.;
//		// { get { CalculationData.Get(_defoult.,  _edit.);  } set { CalculationData.Set(ref _edit., value); } }
//	}
//}
//	
//

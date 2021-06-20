using UnityEngine;
using Core.Player.Movement;
using System;
using Core.Camera.Movement;
using Core.Player.Movement.Data;
using Core.Camera.Movement.Data;
using Core.Player.Characteristics;

namespace Core.PhysicSystem.Objects
{
	public interface IPhysicObject
	{	
        /// <summary>
        /// Take object.
        /// </summary>
		void Take();
	
        /// <summary>
        /// Throw away object.
        /// </summary>
        /// <param name="vector">Throw vector.</param>
		void ThrowAway(Vector3 vector);

        /// <summary>
        /// Throw hard object.
        /// </summary>
        /// <param name="vector">Throw vector</param>
		void ThrowHard(Vector3 vector);
		
        /// <summary>
        /// Move object.
        /// </summary>
		void Move();             
		
        /// <summary>
        /// Update direction move.
        /// </summary>
        /// <param name="targetPosition">Target position.</param>
        /// <param name="moveSpeed">Player current move speed.</param>
		void UpdateDirection(Vector3 targetPosition, float moveSpeed);	
	
        /// <summary>
        /// Change centre of mass this object.
        /// </summary>
        /// <param name="targeCentreOfMass">Target position centre of mass.</param>
		void ChangeCentreOfMass(Vector3 targeCentreOfMass);

        /// <summary>
        /// Check is the object active for interaction.
        /// </summary>
        /// <param name="player">Position player.</param>
        /// <returns>Is object active.</returns>
		bool CheckObjectIsActiveForInteraction(Transform player);

        /// <summary>
        /// Get object position.
        /// </summary>
        /// <returns>Object position.</returns>
		Vector3 GetObjectPosition();

        /// <summary>
        /// Update effects.
        /// </summary>
        /// <param name="enduracne">Endurnce</param>
        void UpdateEffectsWhenMove(EndurancePlayer enduracne);
	}
	
	[Serializable]
	public struct MovementData
	{
		public Rigidbody Rigidbody;
		[Space]
		public bool EditCentreOfMass;		
		[Space]
		[Range(1, 5000)] public float SlowingMove;
		[Range(1, 5000)] public float CollisionSlowingMove;			
		[Space]
		public float TakedMassOfObject;		
		[Space]
		[ReadOnly] public float OriginalMassaObject;
		[ReadOnly] public Vector3 Direction;
		[ReadOnly] public float CurrentSlowingMove;
		
		public void SetTakedMassObject()
		{
			Rigidbody.mass = TakedMassOfObject;
		}
		
		public void ResetTakedMassObject()
		{
			Rigidbody.mass = OriginalMassaObject;
		}
		
		public void ApplyCollisionSlowingMove()
		{
			CurrentSlowingMove = CollisionSlowingMove;
		}
		
		public void ResetSlowingMove()
		{
			CurrentSlowingMove = SlowingMove;
		}
		
		public void SaveOriginalValue()
		{
			CheckExeption();
			
			OriginalMassaObject = Rigidbody.mass;
		}
		
		private void CheckExeption()
		{
			if (Rigidbody.mass == 0)
			{
				throw new ArgumentException("Mass of the object cannot be zero!");
			}
			
			if (TakedMassOfObject == 0)
			{
				throw new ArgumentException("Taked Mass of the object cannot be zero!");
			}
		}
	}
	
	[Serializable]
	public struct DifficultMovementData
	{
		public MovementData StandartMovement;
		
		public bool FreeVelocity;
		public bool BlockFreeRotation;
		public bool BlockMoveY;
		
		public bool UseInterpolate;
		public bool UseContinuousDynamic;
		
		[ReadOnly] public RigidbodyInterpolation OriginalInterpolationMode;
		[ReadOnly] public CollisionDetectionMode OriginalDetectionMode;
		[ReadOnly] public Quaternion StartRotationVector;
		[ReadOnly] public float TakedPositionYAxes;
		
		public Vector3 GetBlockMoveVectorForY(Vector3 direction)
		{
			return new Vector3(direction.x, TakedPositionYAxes, direction.z);
		}
		
		public void ApplyCollisionDetectionMode()
		{
			if (UseContinuousDynamic)
			{
				StandartMovement.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
		}
		
		public void ResetCollisionDetectionMode()
		{
			StandartMovement.Rigidbody.collisionDetectionMode = OriginalDetectionMode;
		}
		
		public void ApplyInterpolation()
		{
			if (UseInterpolate)
			{
				StandartMovement.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
		}
		
		public void ResetInterpolation()
		{
			StandartMovement.Rigidbody.interpolation = OriginalInterpolationMode;
		}
		
		public void SaveOriginalValue()
		{
			OriginalInterpolationMode = StandartMovement.Rigidbody.interpolation;
			OriginalDetectionMode = StandartMovement.Rigidbody.collisionDetectionMode;
			StartRotationVector = StandartMovement.Rigidbody.rotation;
			TakedPositionYAxes = StandartMovement.Rigidbody.position.y;
			
			StandartMovement.SaveOriginalValue();
		}
	}
	
	[Serializable]
	public struct TakeData
	{
		[Range(0.1f, 10)] public float MaxDistancyToObject;
	}
	
	[Serializable]
	public struct ForceThrow
	{
		public Vector3 MaxVectorTorque;
		
		[Range(1, 100)] public float ThrowHard;
		
		[Range(1, 100)] public float ThrowAway;
		[Range(1, 100)] public float ForseTorqueAfterThrowAway;
		
		public Vector3 GetRandomVectorTorque(Vector3 target, Vector3 directionNormalize)
		{
			return new Vector3(UnityEngine.Random.Range(-target.x, target.x) * directionNormalize.x, UnityEngine.Random.Range(-target.y, target.y) * directionNormalize.y, UnityEngine.Random.Range(-target.z, target.z) * directionNormalize.z);
		}
	}
	
	[Serializable]
	public struct CameraRestrictions
	{
		[Header("Min = Look Top | Max = Look Down")]
		public Vector2Angle ClampAngle;
	}
	
	[Serializable]
	public struct PlayerEffects
	{
        public ObjectEnduranceData EnduranceSubstructData;
	}
}

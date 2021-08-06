using UnityEngine;
using System;

namespace Core.PhysicSystem.Objects.Data
{
    [Serializable]
    public struct MovementData
    {
        public Rigidbody Rigidbody;
        public float TakedMassOfObject;

        [Space]
        public Transform PointCollision;

        [Header("Movement")]
        [Range(0.01f, 100)]
        public float SlowingMove;
        [Range(0.01f, 100)]
        public float CollisionSlowingMove;

        [Space]
        [Range(1, 100)]
        public float PlayerSpeedRatio;

        [Space]
        public bool IsFreeVelocity;
        public bool IsBlockMoveY;

        public bool UseInterpolate;
        public bool UseContinuousDynamic;

        [Space]
        [ReadOnly]
        public Vector3 Direction;

        [ReadOnly]
        public float CurrentSlowingMove;

        public Action ApplyBlockMovement;
        public Action ApplyCollisionSettings;

        private RigidbodyInterpolation _originalInterpolationMode;
        private CollisionDetectionMode _originalDetectionMode;

        private float _originalMassaObject;
        private float _takedPositionYAxes;

        public void InitData()
        {
            ApplyBlockMovement = Nothing;
            ApplyCollisionSettings = Nothing;

            if(IsBlockMoveY)
                ApplyBlockMovement += DoBlockMoveY;

            if(!IsFreeVelocity)
                ApplyBlockMovement += DoBlockFreeVelocity;

            if(UseContinuousDynamic)
                ApplyCollisionSettings += DoCollisionDetectionMode;

            if(UseInterpolate)
                ApplyCollisionSettings += DoInterpolation;
        }

        public void ResetCollisionDetectionMode()
        {
            Rigidbody.collisionDetectionMode = _originalDetectionMode;
        }

        public void ResetInterpolation()
        {
            Rigidbody.interpolation = _originalInterpolationMode;
        }

        public void ResetTakedMassObject()
        {
            Rigidbody.mass = _originalMassaObject;
        }

        public void ResetSlowingMove()
        {
            CurrentSlowingMove = SlowingMove;
        }

        public void SetTakedMassObject()
        {
            Rigidbody.mass = TakedMassOfObject;
        }

        public void ApplyCollisionSlowingMove()
        {
            CurrentSlowingMove = CollisionSlowingMove;
        }

        public void SaveOriginalValue()
        {
            CheckExeption();

            _originalMassaObject = Rigidbody.mass;
            _originalInterpolationMode = Rigidbody.interpolation;
            _originalDetectionMode = Rigidbody.collisionDetectionMode;
            _takedPositionYAxes = Rigidbody.position.y;
        }

        private void CheckExeption()
        {
            if(Rigidbody.mass == 0)
            {
                throw new ArgumentException("Mass of the object cannot be zero!");
            }

            if(TakedMassOfObject == 0)
            {
                throw new ArgumentException("Taked Mass of the object cannot be zero!");
            }
        }

        private void DoBlockFreeVelocity()
        {
            Rigidbody.velocity = Vector3.zero;
        }

        private void DoBlockMoveY()
        {
            Direction = GetBlockMoveVectorForY(Direction);
        }

        private void DoCollisionDetectionMode()
        {
            Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        private void DoInterpolation()
        {
            Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private Vector3 GetBlockMoveVectorForY(Vector3 direction)
        {
            return new Vector3(direction.x, _takedPositionYAxes, direction.z);
        }

        private void Nothing(){}
    }
}

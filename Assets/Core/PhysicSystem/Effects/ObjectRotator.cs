using UnityEngine;
using Core.PhysicSystem.Objects;

namespace Core.PhysicSystem.Effects
{
    public delegate float GetAxes(Vector3 point);
    public delegate Vector3 MoveVector(Vector3 source, Vector3 target, float offset);

    public class ObjectRotator : IPhysicObject
    {
        [SerializeField]
        private IPhysicObject _mainPhysicObject;

        [Space]
        [SerializeField]
        private Rigidbody _mainObject;

        [Space]
        [SerializeField]
        private float _speedRotate;
        [SerializeField]
        private float _moveSpeed;

        [Space]
        [SerializeField]
        private Vector3 _direction;

        [SerializeField]
        private Vector3 _axesCheck = Vector3.right;

        [Space]
        [SerializeField]
        private ReverseValue _isReverseDirectionMove = ReverseValue.No;

        [SerializeField]
        private ReverseValue _isReverseTransform = ReverseValue.No;

        [Space]
        [SerializeField]
        private bool _isApplyGravityForce;

        [SerializeField]
        private float _gravityForce;

        [Space]
        [SerializeField]
        private RotatorPoint _point;

        [SerializeField]
        private Transform _targetPoint;

        private GetAxes _getXAxes = (Vector3 point) => { return point.x; };
        private GetAxes _getYAxes = (Vector3 point) => { return point.y; };
        private GetAxes _getZAxes = (Vector3 point) => { return point.z; };

        private MoveVector _getXMoveVector = (Vector3 source, Vector3 target, float offset) => { return new Vector3(target.x + offset, source.y, source.z); };
        private MoveVector _getYMoveVector = (Vector3 source, Vector3 target, float offset) => { return new Vector3(source.x, target.y + offset, source.z); };
        private MoveVector _getZMoveVector = (Vector3 source, Vector3 target, float offset) => { return new Vector3(source.x, source.y, target.z + offset); };

        private MoveVector _getCurrentMoveVector;
        private GetAxes _getCurrentAxes;

        public override void Take()
        {
            _mainPhysicObject.Take();
        }

        public override void PutObject(Vector3 vector)
        {
            _mainPhysicObject.PutObject(vector);
        }

        public override void ThrowObject(Vector3 vector)
        {
            _mainPhysicObject.ThrowObject(vector);
        }

        public override void Move(Vector3 targetPosition, float moveSpeed)
        {
            _targetPoint.position = targetPosition;

            MoveObject(targetPosition, moveSpeed);
        }

        public override bool IsCanTakeObject(Transform player)
        {
            return _mainPhysicObject.IsCanTakeObject(player);
        }

        public override bool IsCanKeepingObject(Transform player)
        {
            return _mainPhysicObject.IsCanKeepingObject(player);
        }

        public override Vector3 GetObjectPosition()
        {
            return _mainPhysicObject.GetObjectPosition();
        }

        public override void SetPointCollision(Vector3 pointCollision)
        {
            _mainPhysicObject.SetPointCollision(pointCollision);
        }

        public override EffectsComponentGroup GetEffects()
        {
            return _mainPhysicObject.GetEffects();
        }

        private void MoveObject(Vector3 targetPosition, float moveSpeed)
        {
            float targetPoint = _getCurrentAxes(_targetPoint.localPosition * (int)_isReverseTransform);
            float rotatorPoint = _getCurrentAxes(_point.PointPosition.localPosition);

            if(targetPoint > rotatorPoint + _point.Offset.Right)
            {
                MoveRotation(ReverseValue.Yes);
                _mainPhysicObject.Move(_getCurrentMoveVector(targetPosition, _mainObject.position, _moveSpeed), moveSpeed);
            }
            else if(targetPoint < rotatorPoint + _point.Offset.Left)
            {
                MoveRotation(ReverseValue.No);
                _mainPhysicObject.Move(_getCurrentMoveVector(targetPosition, _mainObject.position, _moveSpeed * -1), moveSpeed);
            }
        }

        private void MoveRotation(ReverseValue value)
        {
            Vector3 direction = _direction * _speedRotate * (int)_isReverseDirectionMove;

            _mainObject.MoveRotation(_mainObject.rotation * Quaternion.Euler(direction * (int)value * Time.fixedDeltaTime));
        }

        private void Start()
        {
            CheckAxesChecking();
        }

        private void CheckAxesChecking()
        {
            if(_axesCheck.x == 1)
            {
                _getCurrentAxes = _getXAxes;
                _getCurrentMoveVector = _getXMoveVector;
            }
            else if(_axesCheck.y == 1)
            {
                _getCurrentAxes = _getYAxes;
                _getXMoveVector = _getYMoveVector;
            }
            else
            {
                _getCurrentAxes = _getZAxes;
                _getYMoveVector = _getZMoveVector;
            }
        }
    }

    [System.Serializable]
    public struct RotatorPoint
    {
        public Transform PointPosition;
        public Offset Offset;
    }

    [System.Serializable]
    public struct Offset
    {
        public float Left;
        public float Right;
    }

    public enum ReverseValue
    {
        Yes = -1,
        No = 1
    }
}

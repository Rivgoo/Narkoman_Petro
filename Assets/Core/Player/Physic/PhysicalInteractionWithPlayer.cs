using UnityEngine;
using Core.Player.Movement;
using Core.Player.Movement.Data;
using System.Collections.Generic;

namespace Core.Player.Physic
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PlayerPhysic/PhysicalInteractionWithPlayer")]
    public class PhysicalInteractionWithPlayer : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _object;

        [Space]
        [SerializeField]
        private PhysicalInteractionForces _interactionForces;

        private float[] _forceValues = new float[4];

        public void AddForce(Vector3 force, TypeMovement typeMovement)
        {
            _object.AddForce(force * GetForceValue(typeMovement), ForceMode.Force);
        }

        public void UpdateForces()
        {
            _forceValues[0] = 0;
            _forceValues[1] = _interactionForces.Walk;
            _forceValues[2] = _interactionForces.Run;
            _forceValues[3] = _interactionForces.Crouch;
        }

        private float GetForceValue(TypeMovement type)
        {
            return _forceValues[(int)type];
        }

        private void Start()
        {
            UpdateForces();
        }
    }
}

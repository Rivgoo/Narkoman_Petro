using UnityEngine;
using Core.Player.Movement;
using Core.Player.Movement.Data;
using System.Collections.Generic;
using System.Collections;

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

        private bool _isAddForce = false;

        public void AddForce(Vector3 force, TypeMovement typeMovement)
        {
            if(!_isAddForce)
            {
                _isAddForce = true;
                _object.AddForce(force * GetForceValue(typeMovement), ForceMode.Acceleration);

                StartCoroutine(WaitForEndOfFrame());
            }
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

        private IEnumerator WaitForEndOfFrame()
        {
             yield return new WaitForEndOfFrame();

            _isAddForce = false;

            yield break;
        }
    }
}

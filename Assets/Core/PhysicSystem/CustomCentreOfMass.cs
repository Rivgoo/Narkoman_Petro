using UnityEngine;

namespace Core.PhysicSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PlayerPhysic/CustomCenterOfMass")]
    public class CustomCentreOfMass : MonoBehaviour
    {
        [Header("Rigidbody")]
        [SerializeField]
        private Rigidbody _rigidbody;

        [Header("Target Position Center Of Mass")]
        [SerializeField]
        private Transform _targeCentreOfMass;

        private Vector3 _originalCentreOfMass;

        public void ApplyCentreOfMass()
        {
            _rigidbody.centerOfMass = Vector3.Scale(_targeCentreOfMass.localPosition, transform.localScale);
        }

        private void Start()
        {
            ApplyCentreOfMass(); 
        }

        private void OnDrawGizmos()
        {
            if(_rigidbody != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, 0.05f);
            }
        }
    }
}

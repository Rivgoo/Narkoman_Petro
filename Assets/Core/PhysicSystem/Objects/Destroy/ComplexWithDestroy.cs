using UnityEngine;

namespace Core.PhysicSystem.Objects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PhysicObjects/ComplexWithDestroy")]
    public class ComplexWithDestroy : Complex
    {
        [Space]
        [SerializeField]
        private float _velocityValueForDestroy;

        [SerializeField]
        private Destroyed[] _prefabObjects;

        private void OnCollisionEnter(Collision collision)
        {
            ApplyCollisionSlowingMove();

            if (collision.relativeVelocity.magnitude > _velocityValueForDestroy)
            {
                CancelCameraRestrictions();

                InstanceDestroyObject();
                DestroyThis();
            }
        }

        private void InstanceDestroyObject()
        {
            var indexSpawnObject = Random.Range(0, _prefabObjects.Length);

            Instantiate(_prefabObjects[indexSpawnObject], transform.position, transform.rotation);
        }

        private void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}

using Core.PhysicSystem.Objects;
using UnityEngine;

namespace Core.PhysicSystem.Effects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PhysicObjectEffects/DestroyObject")]
    public class DestroyObject : MonoBehaviour
    {
        public event System.Action DestroyTheObject;

        [SerializeField]
        private float _velocityValueForDestroy;

        [SerializeField]
        private Destroyed[] _prefabObjects;

        private bool _isDestroyed = false;

        private void OnCollisionEnter(Collision collision)
        {
            if(!_isDestroyed && collision.relativeVelocity.magnitude > _velocityValueForDestroy)
            {
                _isDestroyed = true;

                DestroyTheObject();

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

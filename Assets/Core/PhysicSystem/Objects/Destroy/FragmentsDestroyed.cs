using UnityEngine;

namespace Core.PhysicSystem.Objects
{
    [RequireComponent(typeof(Rigidbody))]
    public class FragmentsDestroyed : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        public void DestroyScript()
        {
            Destroy(this);
        }

        public void AddForceFragment(float force)
        {
            var direction = new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force));

            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }
    }
}

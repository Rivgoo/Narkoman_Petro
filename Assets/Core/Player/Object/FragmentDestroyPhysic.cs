using UnityEngine;

namespace Objects
{
	public class FragmentDestroyPhysic : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private bool _IsDestroyCollider;
		[SerializeField] private bool _IsDestroyRigidboby;
		
		[Header("Physic")]
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Collider _collider;
		
		public void DestroyPhysic()
		{
			if (_IsDestroyCollider)
			{
				Destroy(_rigidbody);
				Destroy(_collider);
			}
			else if (_IsDestroyRigidboby) 
			{
				Destroy(_rigidbody);
			}

			Destroy(this);
		}
		
		public void AddForceFragment(float force)
		{
			var direction = new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force));
			_rigidbody.AddForce(direction, ForceMode.Impulse);
		}
	}
}
using UnityEngine;

namespace Objects
{
	public class FragmentDestroyPhysic : MonoBehaviour
	{
		[Header("Physic")]
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Collider[] _colliders;
		
		public void DestroyObject()
		{
			Destroy(gameObject);
			Destroy(this);
		}
		
		public void DestroyRigidboby()
		{
			Destroy(_rigidbody);
			Destroy(this);
		}
		
		public void DestroyPhysic()
		{
			Destroy(_rigidbody);
			
			for (int i = 0; i < _colliders.Length; i++)
			{
				Destroy(_colliders[i]);
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
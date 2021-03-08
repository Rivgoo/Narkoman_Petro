using UnityEngine;

namespace Objects
{
	public class FragmentDestroyPhysic : MonoBehaviour
	{
		[SerializeField] private Rigidbody _physic;
		
		public void DestroyPhysic()
		{
			Destroy(_physic);
			Destroy(this);
		}
		
		public void AddForceFragment(float force)
		{
			var direction = new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force));
			_physic.AddForce(direction, ForceMode.Impulse);
		}
	}
}
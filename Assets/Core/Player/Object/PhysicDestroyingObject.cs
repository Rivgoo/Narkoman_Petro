using UnityEngine;

namespace Objects
{
	public class PhysicDestroyingObject : PhysicObjectDefoult
	{
		[Header("Destroy Settings")]
		[SerializeField] private float _minValueForDestroy;
		[SerializeField] private GameObject _destroyedObject;
		
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.relativeVelocity.magnitude > _minValueForDestroy)
			{
				Destroy();
			}
		}
		
		private void Destroy()
		{
			Instantiate(_destroyedObject, transform.position, transform.rotation);
			
			DestroyOriginalObject();
		}
		
		private void DestroyOriginalObject()
		{
			Destroy(gameObject);
		}
	}
}


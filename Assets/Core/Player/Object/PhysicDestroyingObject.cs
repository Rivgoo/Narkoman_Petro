using UnityEngine;

namespace Objects
{
	public class PhysicDestroyingObject : PhysicObjectDefoult
	{
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
			_takeOff = true;
	
			Instantiate(_destroyedObject, transform.position, transform.rotation);
			
			DestroyOriginalObject();
		}
		
		private void DestroyOriginalObject()
		{
			Destroy(gameObject);
		}
	}
}


using UnityEngine;
using SettingsGame;

namespace Objects
{
	public class PhysicDestroyingObject : PhysicObjectDefoult
	{
		[Header("Min Value To Destroy")]
		[SerializeField] private float _minValueToDestroy;
		
		[Header("Prefabs")]
		[SerializeField] private GameObject[] _destroyedObjectsHeight;
		[SerializeField] private GameObject[] _destroyedObjectsMedium;
		
		private bool _isDestroied;
		
		private void OnCollisionEnter(Collision collision)
		{
			if (!_isDestroied)
			{
				if (collision.relativeVelocity.magnitude > _minValueToDestroy)
				{
					Destroy();
				}
			}
		}
		
		private void Destroy()
		{
			_isDestroied = true;
			
			if (SettingsPhysic.Quality.QualityDestroyObject == QualityDestroyObject.Hight)
			{
				var number = Random.Range(0, _destroyedObjectsHeight.Length);
				InstantiateObject(_destroyedObjectsHeight[number]);	
			}
			else
			{
				var number = Random.Range(0, _destroyedObjectsMedium.Length);
				InstantiateObject(_destroyedObjectsMedium[number]);	
			}
			
			DestroyOriginalObject();
		}
			
		private void InstantiateObject(GameObject objectInstantiate)
		{
			Instantiate(objectInstantiate, transform.position, transform.rotation);
		}
		
		private void DestroyOriginalObject()
		{
			Destroy(gameObject);
		}
	}
}


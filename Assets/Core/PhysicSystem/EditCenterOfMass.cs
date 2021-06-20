using UnityEngine;

namespace Core.PhysicSystem
{
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("PlayerPhysic/EditCenterOfMass")]
	public class EditCenterOfMass : MonoBehaviour     
	{
		[Header("Rigidbody")]
		[SerializeField] private Rigidbody _rigidbody;
		
		[Header("Target Position Center Of Mass")]
		[SerializeField] private Transform _targeCentreOfMass;
		
		private Vector3 _originalCentreOfMass;
		
		public void ApplyCentreOfMass(Vector3 targeCentreOfMass)
		{
			_targeCentreOfMass.position = targeCentreOfMass;
			_rigidbody.centerOfMass = Vector3.Scale(_targeCentreOfMass.localPosition, transform.localScale);
		}
		
		public void ResetCentreOfMass()
		{
			_rigidbody.centerOfMass = _originalCentreOfMass;
		}
		
		private void Start()
		{
			SaveOriginalCentreOfMass();
		}
		
		private void SaveOriginalCentreOfMass()
		{
			_originalCentreOfMass = _rigidbody.centerOfMass;
		}

        private void OnDrawGizmos()
        {
            if (_rigidbody != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, 0.05f);
            }
        }
	}
}
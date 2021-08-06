using UnityEngine;

namespace Core.PhysicSystem.Effects
{
	[RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PhysicObjectEffects/EditCenterOfMass")]
	public class EditCenterOfMass : MonoBehaviour     
	{
        public Vector3 TargetCentreOfPosition
        {
            set
            {
                _targeCentreOfMass.position = value;
            }
        }

        [SerializeField]
        private bool _editCentreOfMass;

        [Space]
		[SerializeField] private Rigidbody _rigidbody;
		
		[SerializeField] private Transform _targeCentreOfMass;
		
		private Vector3 _originalCentreOfMass;
		
		public void ApplyCentreOfMass()
		{
            if(_editCentreOfMass)
            {
                _rigidbody.centerOfMass = Vector3.Scale(_targeCentreOfMass.localPosition, transform.localScale);
            }
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
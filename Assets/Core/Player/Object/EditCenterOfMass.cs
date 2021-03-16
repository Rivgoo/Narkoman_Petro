using UnityEngine;

public class EditCenterOfMass : MonoBehaviour
{
	[Header("Rigidbody")]
	[SerializeField] private Rigidbody _rigidbody;
	
	[Header("Target Position Center Of Mass")]
	[SerializeField] private Transform _targetPosition;
	
	private void Awake()
	{
		_rigidbody.centerOfMass = Vector3.Scale(_targetPosition.localPosition, transform.localScale);
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, 0.1f);
	}
	
}

using UnityEngine;

namespace Player
{
	public class PlayerPhysics : MonoBehaviour
	{
		[SerializeField] private float _force;
		
	     private void OnControllerColliderHit(ControllerColliderHit hit)
	     {
	         Rigidbody body = hit.collider.attachedRigidbody;
				   
	          if (body == null || hit.moveDirection.y < -0.3f) return;
	          
	          var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
	          body.AddForce(pushDir * _force, ForceMode.Force);
	     }
		
	}
}
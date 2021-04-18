using UnityEngine;
using PlayerData;

namespace Player
{
	public class PhysicsInteractionSmallObject : MonoBehaviour
	{
		private MovementPlayerData _playerData;
			
		public void Init(MovementPlayerData playerData)
		{
			_playerData = playerData;
		}
		
	    private void OnControllerColliderHit(ControllerColliderHit hit)
	    {
	        Rigidbody body = hit.collider.attachedRigidbody;
				   
	         if (body == null || hit.moveDirection.y < -0.3f) return;
	         
	         var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
	         body.AddForce(pushDir * _playerData.Physic.ForceSmallObject, ForceMode.Force);
	    }
	}
}
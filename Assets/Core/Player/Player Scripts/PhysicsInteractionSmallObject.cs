using UnityEngine;
using PlayerData;

namespace Player
{
	public class PhysicsInteractionSmallObject : MonoBehaviour
	{
		private MovementPlayerData _playerData;
		
		private float _currentForce;
			
		public void Init(MovementPlayerData playerData)
		{
			_playerData = playerData;
		}
		
	    private void OnControllerColliderHit(ControllerColliderHit hit)
	    {
	        Rigidbody body = hit.collider.attachedRigidbody;
				   
	         if (body == null || hit.moveDirection.y < -0.3f) return;
	         
	         UpdateCurrentForce();
	         var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
	         body.AddForce(pushDir * _currentForce, ForceMode.Force);
	    }
	    
	    private void UpdateCurrentForce()
	    {
	    	if (_playerData.State.IsMovePlayer || _playerData.State.IsJumping)
	    	{
	    		_currentForce = 0;
	    		return;
	    	}
	    	
	    	_currentForce = _playerData.Physic.ForceSmallObject;
	    }
	}
}
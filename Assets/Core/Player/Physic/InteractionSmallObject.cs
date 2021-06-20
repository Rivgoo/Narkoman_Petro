using UnityEngine;
using Core.Player.Movement;
using Core.Player.Movement.Data;

namespace Core.Player.Physic
{
	public class InteractionSmallObject : MonoBehaviour
	{
        [SerializeField]
        private float _interactionForce;

        private Rigidbody _targetRigidbody;
		
	    private void OnControllerColliderHit(ControllerColliderHit hit)
	    {
	         _targetRigidbody = hit.collider.attachedRigidbody;
				   
	         if (_targetRigidbody == null) 
                 return;

             _targetRigidbody.AddForce(hit.moveDirection * _interactionForce, ForceMode.Force);
	    }
	}
}
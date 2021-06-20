using UnityEngine;
using Core.Player.Movement;
using Core.Player.Movement.Data;

namespace Core.Player.Physic
{
	public class StandartInteraction : MonoBehaviour
	{
        [SerializeField]
        private PlayerMovementStates _states;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            PhysicalInteractionWithPlayer interaction = hit.gameObject.GetComponent<PhysicalInteractionWithPlayer>() as PhysicalInteractionWithPlayer;

            if(interaction == null)
                return;

            interaction.AddForce(hit.moveDirection, _states.States.CurrentTypeMovement);
        }
	}
}


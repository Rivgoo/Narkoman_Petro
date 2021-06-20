using Core.Player.Movement.Data;
using UnityEngine;

namespace Core.Player.Movement
{
	public class StepsSystem : MonoBehaviour
	{
		[SerializeField] 
        private PlayerMovement _movementPlayer;

        [SerializeField]
        private PlayerMovementStates _state;

        [SerializeField] 
        private PlayerSound _sound;
		
		private void ProgressStepCycle() 
        {
            if (_movementPlayer.Movement.UserInput.x != 0 || _movementPlayer.Movement.UserInput.y != 0)
            {
            	float leghtStep = 1;

                if (_state.States.Walking)
            	{
            		leghtStep = _movementPlayer.Step.WalkStepLenghten;
            	}
                else if (_state.States.Running)
            	{
            		leghtStep = _movementPlayer.Step.RunStepLenghten;
            	}
                else if (_state.States.Crouching)
            	{
            		leghtStep = _movementPlayer.Step.CrouchStepLenghten;
            	}
            	
            	_movementPlayer.Step.StepCycle += (_movementPlayer.Movement.CharacterController.velocity.magnitude + (_movementPlayer.SpeedsValue.Current * leghtStep)) * Time.fixedDeltaTime;
            }
 
            if (!(_movementPlayer.Step.StepCycle > _movementPlayer.Step.NextStep))
            {
                return;
            }

            _movementPlayer.Step.NextStep = _movementPlayer.Step.StepCycle + _movementPlayer.Step.StepInterval;

            PlayFootStepAudio();
        }
		
		private void PlayFootStepAudio()
        {
            if (_movementPlayer.Movement.CharacterController.isGrounded && _state.States.PreviouslyGrounded)
            {
               _sound.PlayFootStepAudio();
            }
        } 
		
		private void FixedUpdate()
		{
            if(_state.States.Moving)
            {
                ProgressStepCycle();
            }
		}
	}
}
using UnityEngine;
using PlayerData; 

namespace Player
{
	public class PlayerSteps : MonoBehaviour
	{
		private MovementPlayerData _movementPlayer;
        private PlayerSound _sound;
        
		public void Init(MovementPlayerData movementPlayer,PlayerSound sounds)
        {
        	_movementPlayer = movementPlayer;
        	_sound = sounds;
        }
		
		private void ProgressStepCycle(float speed) 
        { 
            if (_movementPlayer.State.IsMovePlayer && (_movementPlayer.Move.Input.x != 0 || _movementPlayer.Move.Input.y != 0))
            {
            	float leghtStep = 1;
            	
            	if (_movementPlayer.State.IsWalking)
            	{
            		leghtStep = _movementPlayer.Step.WalkStepLenghten;
            	}
            	else if(_movementPlayer.State.IsRun)
            	{
            		leghtStep = _movementPlayer.Step.RunStepLenghten;
            	}
            	else if(_movementPlayer.State.IsCrouch)
            	{
            		leghtStep = _movementPlayer.Step.CrouchStepLenghten;
            	}
            	
            	_movementPlayer.Step.StepCycle += (_movementPlayer.CharacterController.velocity.magnitude + (speed * leghtStep)) * Time.fixedDeltaTime;
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
            if (_movementPlayer.CharacterController.isGrounded)
            {
               _sound.PlayFootStepAudio();
            }
        } 
		
		private void FixedUpdate()
		{
			ProgressStepCycle(_movementPlayer.Speeds.Current);
		}
	}
}
using UnityEngine;
using Keys = PlayerInput.KeysInput.CheckMovementKey;
using Core.Player.Movement;
using Core.Player.Movement.Data;

namespace Core.Player.Characteristics
{
	public class EndurancePlayer : MonoBehaviour
	{
		[SerializeField] private bool _isUseEndurance;
		
		[Header("Vizualiation Endurancy")]
		[SerializeField] private ShowInfoBarPlayer _showBar;
		
		[Header("Main Endurance Value")]
		[SerializeField] private float _endurance = 100;
		
		[Header("Defoult Value Endurancy")]
		[SerializeField] private EnduranceData _data;
		[Space]
		[SerializeField] private PlayerMovementStates _states;

        private readonly float _minValueForKeep = 1;
		
		public void SubstructEnduranceOfKeepObject(ObjectEnduranceData data)
		{
			SetEndurance(-data.ValueDropRate);
		}
		
        /// <summary>
        /// Subtract endurance of jump.
        /// </summary>
		public void SubtractEnduranceOfJump()
		{
			SetEndurance(-_data.SpeedsDropRate.Jump);
		}

        /// <summary>
        /// Check if player can take object.
        /// </summary>
        /// <returns>if can take.</returns>
		public bool CheckIfCanTake()
		{
			return _endurance > _data.MinValue.TakeObject;
		}

        /// <summary>
        /// Check if player can keep object.
        /// </summary>
        /// <returns>if can keep.</returns>
		public bool CheckIfCanKeep()
		{
            return _endurance > _minValueForKeep;
		}

        /// <summary>
        /// Check if player can jump.
        /// </summary>
        /// <returns>if can jump.</returns>
		public bool CheckCanJump()
        {
			return _endurance > _data.MinValue.Jump && Keys.JumpDown();
        }
        
        /// <summary>
        /// Check if player can run.
        /// </summary>
        /// <returns>if can run.</returns>
       	public bool CheckCanRun() 
        {    	
       		return _states.States.Running && _endurance > 0 && Keys.Run();
        }
       	
        /// <summary>
        /// Check that run key is stay.
        /// </summary>
        /// <returns>Is run key stay.</returns>
       	public bool CheckRunKeyIsStay()
        {
       		if (Keys.RunKeyDown())
        	{
        		return _endurance > _data.MinValue.Run;
        	}     
       		else if (Keys.RunKeyUp() || _endurance == 0)
			{
       			return false;
			} 
			
       		return _states.States.Running; // return the same value
        }
		
		private void SetEndurance(float value)
		{
			if (_isUseEndurance)
			{
				_endurance = Mathf.Clamp(_endurance + value, 0, 100);
				_showBar.UpdateBar(_endurance / 100);		
			}
		}
		
		private void UpdateEndurancyValue()
        {       	
        	if (_states.States.Moving) 
            {
            	if (_states.States.Running) // Player Run
            	{
            		SetEndurance(-_data.SpeedsDropRate.Run);
            	}
            	else if (!_states.States.Crouching) // Don't key down Run
            	{
            		SetEndurance(_data.SpeedsRecovery.Walk);
            	}  	
            }
        	else if(!_states.States.Crouching)
        	{
        		SetEndurance(_data.SpeedsRecovery.Stay);
        	}
        }  
		
		private void FixedUpdate()
		{
			UpdateEndurancyValue();
		}
	}
}

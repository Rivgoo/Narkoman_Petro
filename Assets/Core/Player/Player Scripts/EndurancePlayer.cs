using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey;
using PlayerData;

namespace Player
{
	internal class EndurancePlayer : MonoBehaviour 
	{
		[SerializeField ]private bool _isEndurance;
		
		internal float Endurance { set{ SetEndurance(value); } }
		
		[Header("UI Image Component")]
		[SerializeField] private ShowInfoBarPlayer _showBar;
		
		[Header("Endurance Value")]
		[SerializeField] private float _endurance = 100;
		
		[SerializeField] private EnduranceData _enduranceData;
		
		internal void PlayerRun()
		{
			SetEndurance(_enduranceData.ValueRun);
		}
		
		internal void PlayerJump()
		{
			SetEndurance(_enduranceData.ValueJump);
		}
		
		internal void RecoveryEnduranceDefoult()
		{
			SetEndurance(_enduranceData.RecoveryEnduranceDefoult);
		}
		
		internal void RecoveryEnduranceWalk()
		{
			SetEndurance(_enduranceData.RecoveryEnduranceWalk);
		} 
		
		internal bool CheckIsJump()
        {
			return _endurance > _enduranceData.MinValueForJump && Keys.JumpDown();
        }
        
       	internal bool CheckIsSpeedUp(bool isSpeedUp) 
        {    	
       		return isSpeedUp && _endurance > 0 && Keys.SpeedUp();
        }
       	
       	internal void CheckKeyDownSpeedUp(ref bool isSpeedUp)
        {
       		if (Keys.SpeedUpKeyDown())
        	{
        		isSpeedUp = _endurance > _enduranceData.MinValueForSpeedUp;
        	}     
       		else if (Keys.SpeedUpKeyUp() || _endurance == 0)
			{
       			isSpeedUp = false;
			}       		
        }
		
		private void SetEndurance(float value)
		{
			if (_isEndurance)
			{
				_endurance = Mathf.Clamp(_endurance + value, 0, 100);
				_showBar.UpdateBar(_endurance / 100);
			}
			
		}
	}
}

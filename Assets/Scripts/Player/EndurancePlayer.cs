using UnityEngine;
using Keys = PlayerInputKeys.InputKeys.CheckKey;

namespace Player
{
	internal class EndurancePlayer : MonoBehaviour 
	{
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
			return _endurance > _enduranceData.MinValueForJump && Keys.Jump();
        }
        
       	internal bool CheckIsSpeedUp(bool isSpeedUp)
        {    	
       		return isSpeedUp && _endurance > 0 && Keys.SpeedUp();
        }
       	
       	internal void CheckKeyDownSpeedUp(ref bool isSpeedUp)
        {
       		if (Keys.SpeedUpDown())
        	{
        		isSpeedUp = _endurance > _enduranceData.MinValueForSpeedUp;
        	}
        }
		
		private void SetEndurance(float value)
		{
			_endurance = Mathf.Clamp(_endurance + value, 0, 100);
			_showBar.UpdateBar(_endurance / 100);
		}
	}
}

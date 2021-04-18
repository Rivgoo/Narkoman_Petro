using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey;
using PlayerData.Endurancy;
using PlayerData;

namespace Player
{
	public class EndurancePlayer : MonoBehaviour 
	{
		[SerializeField] private bool _isUseEndurance;
		
		[Header("Vizualiation Endurancy")]
		[SerializeField] private ShowInfoBarPlayer _showBar;
		
		[Header("Main Endurance Value")]
		[SerializeField] private float _endurance = 100;
		
		[Header("Defoult Value Endurancy")]
		[SerializeField] private EnduranceData _defoultData = new EnduranceData();
		
		private EnduranceData _editData = new EnduranceData();
		
		private MovementPlayerData _movementPlayer;
		
		/// <summary>
		/// Init Script
		/// </summary>
		/// <param name="movementPlayer">MovementPlayerData</param>
		public void Init(MovementPlayerData movementPlayer)
        {
        	_movementPlayer = movementPlayer;
        }
		
		public void SetTakeObjectValueDropRate(float valueDropRate)
		{
			SetEndurance(-valueDropRate);
		}
		
		public void SetJumpValueDropRate()
		{
			SetEndurance(-_editData.SpeedsDropRate.Jump);
		}
		
		public bool CheckIsTake()
		{
			return _endurance > _editData.MinValue.TakeObject;
		}
		
		public bool CheckIsTaking()
		{
			return _endurance < 0.5f;
		}
		
		public bool CheckIsJump()
        {
			return _endurance > _editData.MinValue.Jump && Keys.JumpDown();
        }
        
       	public bool CheckIsRun(bool isRun) 
        {    	
       		return isRun && _endurance > 0 && Keys.Run();
        }
       	
       	public void CheckKeyDownRun(ref bool isRun)
        {
       		if (Keys.RunKeyDown())
        	{
        		isRun = _endurance > _editData.MinValue.Run;
        	}     
       		else if (Keys.RunKeyUp() || _endurance == 0)
			{
       			isRun = false;
			}       		
        }
		
		private void SetEndurance(float value)
		{
			if (_isUseEndurance)
			{
				_endurance = Mathf.Clamp(_endurance + value, 0, 100);
				_showBar.UpdateBar(_endurance / 100);		
			}
		}
		
		private void UpdateMovementValueEndurance()
        {       	
        	if (_movementPlayer.State.IsMovePlayer) 
            {
            	if (!_movementPlayer.State.IsWalking) // Player Run
            	{
            		SetEndurance(-_editData.SpeedsDropRate.Run);
            	}
            	else if (!Keys.Run() && !_movementPlayer.State.IsCrouch) // Don't key down Run
            	{
            		SetEndurance(_editData.SpeedsRecovery.Walk);
            	}  	
            }
        	else if(!_movementPlayer.State.IsCrouch)
        	{
        		SetEndurance(_editData.SpeedsRecovery.Stay);
        	}
        }  
		
		private void FixedUpdate()
		{
			if (_isUseEndurance)
			{
				UpdateMovementValueEndurance();
			}
		}
		
		private void Awake()
		{
			InitData();
		}
		
		private void InitData()
		{
			_editData.MinValue = _defoultData.MinValue;
			_editData.SpeedsRecovery = _defoultData.SpeedsRecovery;
			_editData.SpeedsDropRate = _defoultData.SpeedsDropRate;
		}
	}
}

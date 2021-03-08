using UnityEngine;

namespace PlayerData
{
	[System.Serializable]
	[SerializeField]
	public struct EnduranceData
	{
		[Header("Endurance Value")]	
		[SerializeField] internal float ValueRun;
		[SerializeField] internal float ValueJump;
		
		[SerializeField] internal float RecoveryEnduranceDefoult;
		[SerializeField] internal float RecoveryEnduranceWalk;
		
		[SerializeField] internal float MinValueForSpeedUp;
		[SerializeField] internal float MinValueForJump;
		
	}
}
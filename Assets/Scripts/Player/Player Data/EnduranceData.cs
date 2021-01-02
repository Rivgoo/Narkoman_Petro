using UnityEngine;

namespace Player
{
	[System.Serializable]
	[SerializeField]
	internal struct EnduranceData
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
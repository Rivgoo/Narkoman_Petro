using UnityEngine;

namespace Core.Camera.Movement
{
	[System.Serializable]
	public struct BobData 
	{
		[Header("Ranges")]
    	public float BobRangeX;
        public float BobRangeY;
        public float BobRangeZ;
        
        [Header("Accelerations")]
        [Range(1, 5)] public float AccelerationX;
        [Range(1, 5)] public float AccelerationY;
        [Range(1, 5)] public float AccelerationZ;
        
        [Header("Animation Curve")]
        public AnimationCurve AnimatinCurve;
	}
}
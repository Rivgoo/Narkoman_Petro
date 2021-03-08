using UnityEngine;
using Player;  
 
namespace PlayerData
{	
	[System.Serializable]
	[SerializeField]
	public struct ShakeCameraData
	{
        public CurveControlledBob HeadBob;
        [Space]
        public float SpeedCameraLerp;
        
        public float MaxHeightCamera;
        public float MinHeightCamera;
        
        public float MaxXShakeCamera;
        public float MaxZShakeCamera;
        
        [Header("Info")]
        [ReadOnly] public Vector3 CameraVector;
	}
	
	[System.Serializable]
	[SerializeField]
	public struct JumpShakeCamera
	{ 
		public LerpControlledBob JumpBob;
	}
	
	[System.Serializable]
	[SerializeField]
	public class MovementCameraData
	{
		[Header("Main Camera")]
		public Transform Camera;	
		
		public ShakeCameraData Shake;
		public JumpShakeCamera JumpShake;
		
		[Header("Info")]
		[ReadOnly] public Vector3 OriginalCameraPosition;
		
		public MovementCameraData()
		{
			Shake.HeadBob = new CurveControlledBob();
			JumpShake.JumpBob = new LerpControlledBob();
		}
	}
}
	


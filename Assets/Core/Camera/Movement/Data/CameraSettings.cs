using Core.Camera.Effects;
using UnityEngine;
using System;
 
namespace Core.Camera.Movement.Data
{	
	public class CameraSettings : MonoBehaviour
	{
		[Header("Main Camera")]
		public Transform Camera;	
		[Space]
		public Movement Move;
		[Space]
		public ShakeCamera Shake;
		[Space]
		public JumpBob JumpBob;
		[Space]
		public BobTypes Bobs;
		
		public JumpControlledBob JumpShake;
		
		[Header("Info")]
		[ReadOnly] public Vector3 OriginalCameraPosition;
		
		public void Awake()
		{
			Shake.HeadBob = new CurveControlledBob();
            JumpShake = new JumpControlledBob(JumpBob);
		}
	}
}
	


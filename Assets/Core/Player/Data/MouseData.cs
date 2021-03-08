using UnityEngine;

namespace PlayerData
{
	[System.Serializable]
    [SerializeField]
    public struct MouseData
    {
    	[Header("Sensitivity")]
    	[SerializeField ] internal float XSensitivity; 
        [SerializeField ] internal float YSensitivity; 
		
        [Header("Max Angle X")]
        [SerializeField ] internal float MinimumX; 
        [SerializeField ] internal float MaximumX; 
       
        [Header("Interpolate Rotation")]
        [SerializeField ] internal float smoothTime; 
    }
}

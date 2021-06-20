using UnityEngine;

namespace PlayerInput
{
	public class MouseInput : MonoBehaviour
	{
		public static float GetXAxes()
		{
			return Input.GetAxis("Mouse X");
		}
		
		public static float GetYAxes()
		{
			return Input.GetAxis("Mouse Y");
		}
	}
}
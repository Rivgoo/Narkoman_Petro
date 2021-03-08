using UnityEngine;

namespace PlayerInput
{
	public class InputKeys : MonoBehaviour
	{			
		private static DefoultMovementKeys _movementKey;
		
		public static class CheckMovementKey
		{
			internal static DirectionsMove MoveForward()
	        {
	        	if (Input.GetKey(_movementKey.Up) || Input.GetKey(KeyCode.UpArrow))
				{		   		
	            	return DirectionsMove.Up;
				}
				else if (Input.GetKey(_movementKey.Down) || Input.GetKey(KeyCode.DownArrow))
				{					
					return DirectionsMove.Down;
				}
	
				return DirectionsMove.None;
	        }
	        
	        internal static DirectionsMove MoveRight()
	        {
	        	if (Input.GetKey(_movementKey.Right) || Input.GetKey(KeyCode.RightArrow))
				{
					return DirectionsMove.Right;
				} 
				else if (Input.GetKey(_movementKey.Left) || Input.GetKey(KeyCode.LeftArrow))
				{
					return DirectionsMove.Left;
				}
				
				return DirectionsMove.None;
	        }
	        
	        internal static bool JumpDown()
	        {
	        	return Input.GetKeyDown(_movementKey.Jump);
	        }
	        
	        internal static bool SpeedUp()
	        {
	        	return Input.GetKey(_movementKey.SpeedUp);
	        }
	        
	        internal static bool SpeedUpKeyUp()
	        {
	        	return Input.GetKeyUp(_movementKey.SpeedUp);
	        }
	        
	        internal static bool SpeedUpKeyDown()
	        {
	        	return Input.GetKeyDown(_movementKey.SpeedUp);
	        }
	        
	        internal static bool CrouchDown()
	        {
	        	return Input.GetKeyDown(_movementKey.Crouch);
	        }
	        
	        internal static bool CrouchUp()
	        {
	        	return Input.GetKeyUp(_movementKey.Crouch);
	        }
		}
		
		public static class CheckMouseButton
		{
			public static bool Up(MouseButtons buttonName)
			{
				return Input.GetMouseButtonUp((int)buttonName);
			}
			
			public static bool Down(MouseButtons buttonName)
			{
				return Input.GetMouseButtonDown((int)buttonName);
			}
			
			public static bool Stay(MouseButtons buttonName)
			{
				return Input.GetMouseButton((int)buttonName);
			}
			
		}
        
		private void Awake()
		{
			InitMovementKey();
		}
		
		private void InitMovementKey()
		{
			_movementKey.Up = "w";
			_movementKey.Down = "s";
			_movementKey.Right = "d";
			_movementKey.Left = "a";
			_movementKey.Jump = "space";
			_movementKey.SpeedUp = "left shift";
			_movementKey.Crouch = "c";
		}
	}
	
	[System.Serializable]
	public struct DefoultMovementKeys
	{
		public string Up;
		public string Down;
		public string Right;
		public string Left;
		public string Jump;
		public string SpeedUp;
		public string Crouch;
	}
	
	public enum DirectionsMove
	{
		Up = 1, 
		Down = -1,
		Right = 1,
		Left = -1,
		None = 0
	}
	
	public enum MouseButtons
	{
		LeftButton = 0, RightButton = 1, ScrollLock = 2
	}
}



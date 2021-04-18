using UnityEngine;

namespace PlayerInput
{
	public class InputKeys : MonoBehaviour
	{			
		private static DefoultMovementKeys _movementKey;
		private static KeyboardKey _keyboardKey;
		
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
	        
	        internal static bool Run()
	        {
	        	return Input.GetKey(_movementKey.Run);
	        }
	        
	        internal static bool RunKeyUp()
	        {
	        	return Input.GetKeyUp(_movementKey.Run);
	        }
	        
	        internal static bool RunKeyDown()
	        {
	        	return Input.GetKeyDown(_movementKey.Run);
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
		
		public static class CheckKeyboardKey
		{		
			public static bool DownKeyGet()
			{
				return Input.GetKeyDown(_keyboardKey.Get);
			}
			
			public static bool StayKeyGet()
			{
				return Input.GetKey(_keyboardKey.Get);
			}
			
			public static bool UpKeyGet()
			{
				return Input.GetKeyUp(_keyboardKey.Get);
			}	
		}
        
		private void Awake()
		{
			InitMovementKey();
			InitKeyboardKey();
		}
		
		private void InitMovementKey()
		{
			_movementKey.Up = "w";
			_movementKey.Down = "s";
			_movementKey.Right = "d";
			_movementKey.Left = "a";
			_movementKey.Jump = "space";
			_movementKey.Run = "left shift";
			_movementKey.Crouch = "c";
		}
		
		private void InitKeyboardKey()
		{
			_keyboardKey.Get = "e";
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
		public string Run;
		public string Crouch;
	}
	
	[System.Serializable]
	public struct KeyboardKey
	{
		public string Get;
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



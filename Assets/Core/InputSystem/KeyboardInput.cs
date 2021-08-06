using UnityEngine;
using Core.InputSystem.PlayerKeys;

namespace Core.InputSystem
{
	public class KeyboardInput : MonoBehaviour
	{
        [SerializeField]
		private Keyboard _keys;

        public DirectionsMove MoveForward()
	        {
                if(Input.GetKey(_keys.Move.Up) || Input.GetKey(KeyCode.UpArrow))
				{		   		
	            	return DirectionsMove.Up;
				}
				else if (Input.GetKey(_keys.Move.Down) || Input.GetKey(KeyCode.DownArrow))
				{					
					return DirectionsMove.Down;
				}
	
				return DirectionsMove.None;
	        }

        public DirectionsMove MoveRight()
        {
            if(Input.GetKey(_keys.Move.Right) || Input.GetKey(KeyCode.RightArrow))
            {
                return DirectionsMove.Right;
            }
            else if(Input.GetKey(_keys.Move.Left) || Input.GetKey(KeyCode.LeftArrow))
            {
                return DirectionsMove.Left;
            }

            return DirectionsMove.None;
        }

        public bool JumpDown()
        {
            return Input.GetKeyDown(_keys.Move.Jump);
        }

        public bool Run()
        {
            return Input.GetKey(_keys.Move.Run);
        }

        public bool RunKeyUp()
        {
            return Input.GetKeyUp(_keys.Move.Run);
        }

        public bool RunKeyDown()
        {
            return Input.GetKeyDown(_keys.Move.Run);
        }

        public bool CrouchDown()
        {
            return Input.GetKeyDown(_keys.Move.Crouch);
        }

        public bool CrouchUp()
        {
            return Input.GetKeyUp(_keys.Move.Crouch);
        }
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



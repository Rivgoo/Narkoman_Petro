using UnityEngine;

namespace PlayerInputKeys
{
	internal class InputKeys : MonoBehaviour
	{		
		[Header("Input Key Value")]
		[SerializeField] private  DefoultMoveKey Move;
		
		private static DefoultMoveKey _moveKey;
		
		internal static class CheckKey
		{
			internal static int MoveForward()
	        {
	        	if (Input.GetKey(_moveKey.Up) || Input.GetKey(KeyCode.UpArrow))
				{		   		
	            	return 1;
				}
				else if (Input.GetKey(_moveKey.Down) || Input.GetKey(KeyCode.DownArrow))
				{					
					return -1;
				}
	
				return 0;
	        }
	        
	        internal static int MoveRight()
	        {
	        	if (Input.GetKey(_moveKey.Right) || Input.GetKey(KeyCode.RightArrow))
				{
					return 1;
				} 
				else if (Input.GetKey(_moveKey.Left) || Input.GetKey(KeyCode.LeftArrow))
				{
					return -1;
				}
				
				return 0;
	        }
	        
	        internal static bool Jump()
	        {
	        	return Input.GetKeyDown(_moveKey.Jump);
	        }
	        
	        internal static bool SpeedUp()
	        {
	        	return Input.GetKey(_moveKey.SpeedUp);
	        }
	        
	        internal static bool SpeedUpDown()
	        {
	        	return Input.GetKeyDown(_moveKey.SpeedUp);
	        }
	        
	        internal static bool SquattingDown()
	        {
	        	return Input.GetKeyDown(_moveKey.Squatting);
	        }
	        
	        internal static bool SquattingUp()
	        {
	        	return Input.GetKeyUp(_moveKey.Squatting);
	        }
		}
        
		private void Awake()
		{
//			if (!PlayerPrefs.HasKey("IsKey"))
//			{
				DefoultKey();
//			}
//			else if (PlayerPrefs.GetInt("IsKey") == 0)
//			{
				GetKey();
			//}
		}
		
		private void DefoultKey()
		{
			PlayerPrefs.SetInt("IsKey", 0);
			PlayerPrefs.SetString("Up", Move.Up.ToLower());
			PlayerPrefs.SetString("Down", Move.Down.ToLower());
			PlayerPrefs.SetString("Left", Move.Left.ToLower());
			PlayerPrefs.SetString("Right", Move.Right.ToLower());
			PlayerPrefs.SetString("Jump", Move.Jump.ToLower());
			PlayerPrefs.SetString("SpeedUp", Move.SpeedUp.ToLower());
			PlayerPrefs.SetString("Squatting", Move.Squatting.ToLower());
		}
		
		private void GetKey()
		{
			Move.Up = PlayerPrefs.GetString("Up");
			Move.Down = PlayerPrefs.GetString("Down");
			Move.Left = PlayerPrefs.GetString("Left");
			Move.Right = PlayerPrefs.GetString("Right");
			Move.Jump = PlayerPrefs.GetString("Jump");
			Move.SpeedUp = PlayerPrefs.GetString("SpeedUp");
			Move.Squatting = PlayerPrefs.GetString("Squatting");
			
			_moveKey = Move;
		}
	}
	
	[System.Serializable]
	internal struct DefoultMoveKey
	{
		[SerializeField] internal string Up;
		[SerializeField] internal string Down;
		[SerializeField] internal string Right;
		[SerializeField] internal string Left;
		[SerializeField] internal string Jump;
		[SerializeField] internal string SpeedUp;
		[SerializeField] internal string Squatting;
		
	}

}



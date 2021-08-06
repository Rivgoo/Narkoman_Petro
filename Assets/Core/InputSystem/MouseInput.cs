using UnityEngine;
using Core.InputSystem.PlayerKeys;

namespace Core.InputSystem
{
	public class MouseInput : MonoBehaviour
	{
        [SerializeField]
        private Mouse _keys;

		public float GetXAxes()
		{
			return Input.GetAxis(_keys.AxesX);
		}
		
		public float GetYAxes()
		{
            return Input.GetAxis(_keys.AxesY);
		}

        public bool Up(MouseButtons buttonName)
        {
            return Input.GetMouseButtonUp((int)buttonName);
        }

        public bool Down(MouseButtons buttonName)
        {
            return Input.GetMouseButtonDown((int)buttonName);
        }

        public bool Stay(MouseButtons buttonName)
        {
            return Input.GetMouseButton((int)buttonName);
        }
	}
}
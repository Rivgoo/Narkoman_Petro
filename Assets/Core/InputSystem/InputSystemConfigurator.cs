using UnityEngine;
using Core.InputSystem.PlayerKeys;

namespace Core.InputSystem
{
    public class InputSystemConfigurator : MonoBehaviour
    {
        public Mouse MouseKeysCurrent;
        public Keyboard KeyboardKeysCurrent;

        [SerializeField]
        private Mouse _defoultMouseKeys;

        [SerializeField]
        private Keyboard _defoultKeyboardKeys;

        public void ResetKeys()
        {
            ResetKeyboardKeys();
            ResetMouseKey();
        }

        private void ResetMouseKey()
        {
           MouseKeysCurrent.ApplyKeys( _defoultMouseKeys);
        }

        private void ResetKeyboardKeys()
        {
           KeyboardKeysCurrent.ApplyKeys(_defoultKeyboardKeys);
        }

        private void Start()
        {
            ResetKeys();
        }

        //private void OnGUI()
        //{
        //    Event key = Event.current;

        //    if(key.isKey)
        //    {
        //        test = key.keyCode;
        //        print(test);
        //    }
        //}
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Core.GameUI.Object.Button
{
    public class ClickHandlerGroup : MonoBehaviour
    {
        [SerializeField]
        private List<ClickHandler> _clickHandlers = new List<ClickHandler>();

        [Space]
        [SerializeField] [ReadOnly]
        private StandartButton _currentActiveButton;

        private void Clicked(ClickHandler handler, StandartButton button)
        {
            if(_currentActiveButton != null)
                _currentActiveButton.ContinueEffects();

            _currentActiveButton = button;
            
            button.PauseEffects();
        }

        private void Start()
        {
            foreach(var handler in _clickHandlers)
            {
                handler.Click += Clicked;
            }
        }
    }
}

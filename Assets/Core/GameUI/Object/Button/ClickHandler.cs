using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.GameUI.Object.Button
{
    [RequireComponent(typeof(StandartButton))]
    public class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public event System.Action<ClickHandler, StandartButton> Click;

        [SerializeField]
        private StandartButton _button;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(Click != null)
            {
                Click.Invoke(this, _button);
            }
        }
    }
}

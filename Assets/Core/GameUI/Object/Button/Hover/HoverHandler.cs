using UnityEngine;
using UnityEngine.EventSystems;  
using System;

namespace Core.GameUI.Object.Button.Hover
{
    public class HoverHandler : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
    {
        public event Action OnCursorEnter;
        public event Action OnCursorExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
           if(OnCursorEnter != null)
           {
                OnCursorEnter.Invoke();
           }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(OnCursorExit != null)
            {
                OnCursorExit.Invoke();
            }
        }
    }
}

using UnityEngine;

namespace Core.GameUI.Object.Button.Hover
{   
    [RequireComponent(typeof(HoverHandler))]
    public abstract class HoveringEffect : MonoBehaviour
    {
        [SerializeField]
        private HoverHandler _hoverHandler;
        [SerializeField]
        private bool _isReturnToDefoultOnDisable;

        public abstract void DoEffect();
        public abstract void StopEffect();

        public abstract void SetDefoultState();

        protected virtual void Awake()
        {
            SubscribeHoverEvents();
        }

        public abstract void PauseEffect();
        public abstract void ContinueEffect();

        private void SubscribeHoverEvents()
        {
            _hoverHandler.OnCursorEnter += DoEffect;
            _hoverHandler.OnCursorExit += StopEffect;
        }

        private void OnDisable()
        {
            if(_isReturnToDefoultOnDisable)
            {
                SetDefoultState();
            }
        }
    }
}

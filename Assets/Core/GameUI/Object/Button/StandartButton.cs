using UnityEngine;

namespace Core.GameUI.Object.Button
{
    public class StandartButton : MonoBehaviour
    {
        [SerializeField]
        private Hover.HoveringEffect _hoverEffect;

        public void PauseEffects()
        {
            _hoverEffect.PauseEffect();
        }

        public void ContinueEffects()
        {
            _hoverEffect.ContinueEffect();
        }
    }
}

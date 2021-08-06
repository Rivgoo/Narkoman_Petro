using UnityEngine;

namespace Core.GameUI.Main
{
    public abstract class IPanel : MonoBehaviour
    {
        /// <summary>
        /// Show this panel.
        /// </summary>
        public abstract void Show();

        /// <summary>
        /// Hide this panel.
        /// </summary>
        public abstract void Hide();
    }
}

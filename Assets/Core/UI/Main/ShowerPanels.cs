using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.UI.Main
{
    public class ShowerPanels : MonoBehaviour
    {
        [SerializeField]
        private IPanel _start;

        [SerializeField]
        private IPanel _settings;

        [SerializeField]
        private IPanel _graphSettings;

        [SerializeField]
        private IPanel _physicSettings;

        [SerializeField]
        private IPanel _gameSettings;

        /// <summary>
        /// Hide all panels.
        /// </summary>
        public void HideAll()
        {
            HideStart();
            HideSettings();
            HideGameSettings();
            HideGraphSettings();
            HidePhysicSettings();
        }

        /// <summary>
        /// Show start panel.
        /// </summary>
        public void ShowStart()
        {
            _start.Show();
        }

        /// <summary>
        /// Hide start panel.
        /// </summary>
        public void HideStart()
        {
            _start.Hide();
        }

        /// <summary>
        /// Show settings panel.
        /// </summary>
        public void ShowSettings()
        {
            _settings.Show();
        }

        /// <summary>
        /// Hide settings panel.
        /// </summary>
        public void HideSettings()
        {
            _settings.Hide();
        }

        /// <summary>
        /// Show game settings panel.
        /// </summary>
        public void ShowGameSettings()
        {
            _gameSettings.Show();
        }

        /// <summary>
        /// Hide game settings panel.
        /// </summary>
        public void HideGameSettings()
        {
            _gameSettings.Hide();
        }

        /// Show physic settings panel.
        /// </summary>
        public void ShowPhysicSettings()
        {
            _physicSettings.Show();
        }

        /// <summary>
        /// Hide physic settings panel.
        /// </summary>
        public void HidePhysicSettings()
        {
            _physicSettings.Hide();
        }

        /// Show graph settings panel.
        /// </summary>
        public void ShowGraphSettings()
        {
            _graphSettings.Show();
        }

        /// <summary>
        /// Hide graph settings panel.
        /// </summary>
        public void HideGraphSettings()
        {
            _graphSettings.Hide();
        }

        public void DEMOPLAY()
        {
            SceneManager.LoadScene("Demo");
        }
    }
}

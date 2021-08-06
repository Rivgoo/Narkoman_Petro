using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameUI.Main
{
    public class MainPanelsShower : MonoBehaviour
    {
        [SerializeField]
        private IPanel _start;

        [SerializeField]
        private IPanel _settings;

        [Space]
        [SerializeField]
        private float _timeStart;

        [SerializeField]
        private float _timeSettings;

        [Space]
        [SerializeField]
        private TransitionPanel _transitionPanel;

        [Space]
        [SerializeField]
        private Core.Game.MenuPostProcessingChanger _postProcessingChanger;

        public void BeginShowStart()
        {
            _transitionPanel.Show(_timeStart);
            _transitionPanel.Showed += ShowStart;
        }

        public void BeginShowSettings()
        {
            _transitionPanel.Show(_timeSettings);
            _transitionPanel.Showed += ShowSettings;
        }

        private void ShowStart()
        {
            _start.Show();
            _settings.Hide();
            _transitionPanel.Showed -= ShowStart;
            _transitionPanel.Hide(_timeStart);

            _postProcessingChanger.OnMainMenuPostProcessing();
        }

        private void ShowSettings()
        {
            _settings.Show();
            _start.Hide();
            _transitionPanel.Showed -= ShowSettings;
            _transitionPanel.Hide(_timeSettings);

            _postProcessingChanger.OnSettingsMenuPostProcessing();
        }

        public void DEMOPLAY()
        {
            SceneManager.LoadScene("LoadGame");
        }
    }
}

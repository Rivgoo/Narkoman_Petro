using UnityEngine;
using UnityEngine.PostProcessing;

namespace Core.Game
{
    public class MenuPostProcessingChanger  : MonoBehaviour
    {
        [SerializeField]
        private PostProcessingBehaviour _mainPostProcessing;

        [Space]
        [SerializeField]
        private PostProcessingProfile _mainMenu;

        [SerializeField]
        private PostProcessingProfile _settingsMenu;

        public void OnMainMenuPostProcessing()
        {
            _mainPostProcessing.profile = _mainMenu;
        }

        public void OnSettingsMenuPostProcessing()
        {
            _mainPostProcessing.profile = _settingsMenu;
        }
    }
}

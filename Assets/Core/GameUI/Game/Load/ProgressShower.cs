using UnityEngine;
using System.Collections;

namespace Core.GameUI.Game.Load
{
    public class ProgressShower : MonoBehaviour
    {
        [SerializeField]
        private GameUI.Text.LetterGroup _logoText;

        [Space]
        [SerializeField]
        private UnityEngine.UI.Image _loadBar;

        [SerializeField]
        private UnityEngine.UI.Image _background;

        [SerializeField]
        private UnityEngine.UI.Text _loadingGame;

        [SerializeField]
        private UnityEngine.UI.Text _pressAnyKey;

        [Space]
        [SerializeField]
        private Color _backgroundColor;

        [SerializeField]
        private Color _logoColor;

        public void ShowLoadingGameText(float timeShow)
        {
            StartCoroutine(ShowAdditionalText(timeShow));
        }

        public void ChangeElementsColor()
        {
            _background.color = _backgroundColor;
            _loadBar.color = Color.white;
            _logoText.UpdateLettersColor(_logoColor);
        }

        public void UpdateProgressBar(float progress)
        {
            _loadBar.fillAmount = progress;
        }

        public void FillBarComletely()
        {
            _loadBar.fillAmount = 1;
        }

        public void ShowPressAnyKeyText()
        {
            _pressAnyKey.enabled = true;
            _loadingGame.enabled = false;
        }

        private IEnumerator ShowAdditionalText(float timeShow)
        {
            float time = 0;
            float targetAlfa = 0;

            while(targetAlfa <= 1f)
            {
                targetAlfa = time / timeShow;

                TransitionColor(targetAlfa);

                time += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private void TransitionColor(float timeTransition)
        {
            _loadingGame.color = GameUI.ColorChanger.SetAlphaChannel(_loadingGame.color, Mathf.Clamp(timeTransition, 0, 1));
        }
    }
}
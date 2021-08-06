using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Core.GameUI.Main
{
    public class TransitionPanel : MonoBehaviour
    {
        public event System.Action Hided;
        public event System.Action Showed;

        [SerializeField]
        private Image _transitionPanel;

        [SerializeField]
        private float _timeShowTransition;

        [SerializeField]
        private float _timeHideTransition;

        public void Show()
        {
            StartCoroutine(ShowTransition());
        }

        public void Hide()
        {
            StartCoroutine(HideTransition());
        }

        public void Show(float time)
        {
            _timeShowTransition = time;
            StartCoroutine(ShowTransition());
        }

        public void Hide(float time)
        {
            _timeHideTransition = time;
            StartCoroutine(HideTransition());
        }

        private IEnumerator ShowTransition()
        {
            _transitionPanel.raycastTarget = true;

            float time = 0;
            float targetAlfa = 0;

            while(targetAlfa <= 1f)
            {
                targetAlfa = time / _timeShowTransition;

                TransitionColor(targetAlfa);

                time += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            if(Showed != null)
            {
                Showed.Invoke();
            }

            yield break;
        }

        private IEnumerator HideTransition()
        {
            float time = 0;
            float targetAlfa = 0;

            while(targetAlfa >= 0)
            {
                targetAlfa = 1 - (time / _timeHideTransition);

                TransitionColor(targetAlfa);

                time += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            _transitionPanel.raycastTarget = false;

            if(Hided != null)
            {
                Hided.Invoke();
            }

            yield break;
        }

        private void TransitionColor(float timeTransition)
        {
            _transitionPanel.color = GameUI.ColorChanger.SetAlphaChannel(_transitionPanel.color, Mathf.Clamp(timeTransition, 0, 1));
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Core.GameUI.Object.Button.Hover
{
    public class StandartHoverEffect : HoveringEffect
    {
        [Space]
        [SerializeField]
        private UnityEngine.UI.Text _text;

        [SerializeField]
        private UnityEngine.UI.Image _background;

        [SerializeField]
        private UnityEngine.UI.Image _border;

        [Space]
        [SerializeField]
        private float _speedHoverEnter;

        [SerializeField]
        private float _speedHoverExit;

        [Space]
        [SerializeField]
        private Color _textHover;

        [SerializeField]
        private Color _borderHover;

        [SerializeField]
        private Color _backgroundHover;

        private Color _textOriginal;
        private Color _backgroundOriginal;
        private Color _borderOriginal;

        private bool _playEffect;
        private bool _pause;

        public override void DoEffect()
        {
            _playEffect = true;
            StartCoroutine(Do());
        }

        public override void StopEffect()
        {
            _playEffect = false;
            StartCoroutine(Stop());
        }

        public override void PauseEffect()
        {
            _pause = true;
            DoEffect();
        }

        public override void ContinueEffect()
        {
            _pause = false;
            StopEffect();
        }

        public override void SetDefoultState()
        {
            _background.color = _backgroundOriginal;
            _text.color = _textOriginal;
            _border.color = _borderOriginal;
        }

        private IEnumerator Do()
        {
            while(_playEffect)
            {
                UpdateColors(_textHover, _backgroundHover,_borderHover, _speedHoverEnter);

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private IEnumerator Stop()
        {
            while(!_playEffect && !_pause)
            {
                UpdateColors(_textOriginal, _backgroundOriginal, _borderOriginal, _speedHoverExit);

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private void UpdateColors(Color targetText, Color targetBackground, Color targetBorder, float speed)
        {
            _text.color = Color.Lerp(_text.color, targetText, speed * Time.deltaTime);

            _background.color = Color.Lerp(_background.color, targetBackground, speed * Time.deltaTime);

            _border.color = Color.Lerp(_border.color, targetBorder, speed * Time.deltaTime);
        }

        private void SaveOriginalColors()
        {
            _textOriginal = _text.color;
            _backgroundOriginal = _background.color;
            _borderOriginal = _border.color;
        }

        private void Start()
        {
            SaveOriginalColors();
        }
    }
}

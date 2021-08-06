using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Core.GameUI.Object.Button.Hover
{
    public class HoverEffectForLetters : HoveringEffect
    {
        [Space]
        [SerializeField]
        private GameUI.Text.LetterGroup _letterGroup;

        [Space]
        [SerializeField]
        private UnityEngine.UI.Image _background;

        [SerializeField]
        private UnityEngine.UI.Image _border;

        [Space]
        [SerializeField]
        private bool _isRandomColor;

        [SerializeField]
        private float _speedRandomColorLerp;

        [SerializeField]
        private float _updateColorTime;

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

        private List<Color> _randomColors = new List<Color>();

        private bool _playEffect;
        private bool _pause;

        public override void DoEffect()
        {
            _playEffect = true;

            if(_isRandomColor)
            {
                StartCoroutine(UpdateRandomColor());
                StartCoroutine(DoEffectWithRandomColor());
            }
            else
            {
                StartCoroutine(Do());
            }

             
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
            _letterGroup.UpdateLettersColor(_textOriginal);
            _border.color = _borderOriginal;
        }

        private IEnumerator DoEffectWithRandomColor()
        {
            while(_playEffect)
            {
                _letterGroup.UpdateLettersColor(_randomColors, _speedRandomColorLerp);  
                UpdateNotRandomElement(_backgroundHover, _borderHover, _speedHoverEnter);

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private IEnumerator Do()
        {
            while(_playEffect)
            {
                UpdateAllElement(_textHover, _backgroundHover, _borderHover, _speedHoverEnter);

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private IEnumerator Stop()
        {
            while(!_playEffect && !_pause)
            {
                UpdateAllElement(_textOriginal, _backgroundOriginal , _borderOriginal, _speedHoverExit);

                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        private IEnumerator UpdateRandomColor()
        {
            while(_playEffect && _isRandomColor)
            {
                _randomColors.Clear();

                for(var i = 0;i < _letterGroup.Letters.Count;i++)
                {
                    _randomColors.Add(GameUI.ColorChanger.GetRandomColor());
                }

                yield return new WaitForSeconds(_updateColorTime);
            }

            yield break;
        } 

        private void UpdateNotRandomElement(Color targetBackground, Color targetBorder, float speed)
        {
            _background.color = GameUI.ColorChanger.Lerp(_background.color, targetBackground, speed);
            _border.color = GameUI.ColorChanger.Lerp(_border.color, targetBorder, speed);
        }

        private void UpdateAllElement(Color targetText, Color targetBackground, Color targetBorder, float speed)
        {
            _letterGroup.UpdateLettersColor(targetText, speed);

            _background.color = GameUI.ColorChanger.Lerp(_background.color, targetBackground, speed);
            _border.color = GameUI.ColorChanger.Lerp(_border.color, targetBorder, speed);
        }

        private void SaveOriginalColors()
        {
            _backgroundOriginal = _background.color;
            _borderOriginal = _border.color;
            _textOriginal = _letterGroup.Letters[0].Color;
        }

        private void Start()
        {
            SaveOriginalColors();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Core.GameUI.Text
{
    [RequireComponent(typeof(UnityEngine.UI.GridLayoutGroup), typeof(LetterCreator))]
    public class LetterGroup : MonoBehaviour
    {
        public string Text
        {
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    _text = value;
                }
                else
                {
                   throw new System.ArgumentException("value", "Text should not be null or empty!");
                }
            }
        }

        [SerializeField]
        [ReadOnly]
        public List<Letter> Letters = new List<Letter>();

        [Space]
        [TextArea]
        [SerializeField]
        private string _text;

        [Space]
        [SerializeField]
        private Letter _letterPrefab;

        [SerializeField]
        private LetterCreator _lettorCreator;

        public void CreateLetterObjects()
        {
           if(Letters.Count > 0)
           {
               _lettorCreator.DeleteLetters(Letters);
           }

           Letters = _lettorCreator.CreateLetterObjects(_letterPrefab, _text);
        }

        public void DeleteLettersEditor()
        {
            _lettorCreator.DeleteLettersEditor(Letters);
        }

        public void UpdateLettersColor(Color target, float speed)
        {
            for(var i = 0;i < Letters.Count;i++)
            {
               Letters[i].Color = GameUI.ColorChanger.Lerp(Letters[i], target, speed);
            }
        }

        public void UpdateLettersColor(Color target)
        {
            for(var i = 0;i < Letters.Count;i++)
            {
                Letters[i].Color = target;
            }
        }

        public void UpdateLettersColor(List<Color> targetColors, float speed)
        {
            for(var i = 0;i < Letters.Count;i++)
            {
                Letters[i].Color = GameUI.ColorChanger.Lerp(Letters[i], targetColors[i], speed);
            }
        }

        private void Start()
        {
            CreateLetterObjects();
        }
    }
}

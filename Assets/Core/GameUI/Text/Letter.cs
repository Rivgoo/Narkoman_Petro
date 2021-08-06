using UnityEngine;
using System;

namespace Core.GameUI.Text
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class Letter : MonoBehaviour
    {
        public Transform LetterTransform
        {
            get
            {
                return transform;
            }
        }
        public string NameObject
        {
            set
            {
                gameObject.name = value;
            }
        }
        public Color Color
        {
            get
            {
                return _text.color;
            }

            set
            {
                _text.color = value;
            }
        }

        [SerializeField]
        private UnityEngine.UI.Text _text;

        public void SetText(string letter)
        {
            _text.text = letter;
        }

        public void DestroyLetterEditor()
        {
            DestroyImmediate(gameObject);
        }

        public void DestroyLetter()
        {
            Destroy(gameObject);
        }
    }
}

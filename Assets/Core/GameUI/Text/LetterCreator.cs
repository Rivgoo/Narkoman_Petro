using UnityEngine;
using System.Collections.Generic;

namespace Core.GameUI.Text
{
    public class LetterCreator : MonoBehaviour
    {
        public List<Letter> CreateLetterObjects(Letter prefab, string text)
        {
            List<Letter> letters = new List<Letter>();

            for(var i = 0;i < text.Length;i++)
            {
                letters.Add(Instantiate(prefab, transform));

                string letter = text[i].ToString();

                letters[i].SetText(letter);
                letters[i].NameObject = letter;

                transform.SetParent(letters[i].LetterTransform);
            }

            return letters;
        }

        public void DeleteLetters(List<Letter> Letters)
        {
            for(var i = 0;i < Letters.Count;i++)
            {
                Letters[i].DestroyLetter();
            }

            Letters.Clear();
        }

        public void DeleteLettersEditor(List<Letter> Letters)
        {
            for(var i = 0;i < Letters.Count;i++)
            {
                Letters[i].DestroyLetterEditor();
            }

            Letters.Clear();
        }
    }
}

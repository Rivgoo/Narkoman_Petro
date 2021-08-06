using UnityEngine;

namespace Core.GameUI.Object.Settings
{
    public class ValueShower : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Text _text;

        [SerializeField]
        private ValueSetting _values;

        public void UpdateValue(int index)
        {
            _text.text = _values.Values[index].ToUpper();
        }
    }
}

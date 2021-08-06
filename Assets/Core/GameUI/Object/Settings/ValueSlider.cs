using UnityEngine;
using UnityEngine.UI;

namespace Core.GameUI.Object.Settings
{
    public class ValueSlider : MonoBehaviour
    {
        public int ValueIndex
        {
            set
            {
                _currentValueIndex = value;
                _shower.UpdateValue(_currentValueIndex);
            }
        }

        [SerializeField]
        private ValueShower _shower;

        [Space]
        [SerializeField]
        private int _maxValueIndex;

        [SerializeField]
        private bool _blockFreeSlide;

        [SerializeField] [ReadOnly]
        private int _currentValueIndex;

        public void NextValue()
        {
            if(++_currentValueIndex > _maxValueIndex && !_blockFreeSlide)
            {
                SetCurrentValueIndex(0);
            }
            else
            {
                SetCurrentValueIndex();
            }
        }

        public void BackValue()
        {
            if(--_currentValueIndex < 0 && !_blockFreeSlide)
            {
                SetCurrentValueIndex(_maxValueIndex);
            }
            else
            {
                SetCurrentValueIndex();
            }
        }

        private void SetCurrentValueIndex(int index)
        {
            _currentValueIndex = index;
            _shower.UpdateValue(_currentValueIndex);
        }

        private void SetCurrentValueIndex()
        {
            _currentValueIndex = Mathf.Clamp(_currentValueIndex, 0, _maxValueIndex);
            _shower.UpdateValue(_currentValueIndex);
        }
    }
}

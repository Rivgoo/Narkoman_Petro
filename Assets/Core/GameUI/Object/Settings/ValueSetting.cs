using UnityEngine;

namespace Core.GameUI.Object.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObject/ValueSetting", order = 1)]
    public class ValueSetting : ScriptableObject
    {
        public string[] Values;
    }
}

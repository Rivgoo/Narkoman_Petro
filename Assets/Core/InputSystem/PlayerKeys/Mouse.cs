using UnityEngine;

namespace Core.InputSystem.PlayerKeys
{
    [CreateAssetMenu(fileName = "MouseKeys", menuName = "ScriptableObject/KeysData/MouseKeys", order = 2)]
    public class Mouse : ScriptableObject
    {
        public string AxesX;
        public string AxesY;

        public static void ApplyMouseKeys(Mouse sourceKeys, Mouse targetKeys)
        {
            sourceKeys.AxesX = targetKeys.AxesX;
            sourceKeys.AxesY = targetKeys.AxesY;
        }

        public void ApplyKeys(Mouse targetKeys)
        {
            AxesX = targetKeys.AxesX;
            AxesY = targetKeys.AxesY;
        }
    }
}

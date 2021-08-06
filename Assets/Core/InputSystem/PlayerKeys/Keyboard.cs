using UnityEngine;

namespace Core.InputSystem.PlayerKeys
{
    [CreateAssetMenu(fileName = "KeyboardKeys", menuName = "ScriptableObject/KeysData/KeyboardKeys", order = 1)]
    public class Keyboard : ScriptableObject
    {
        public Move Move;

        public static void ApplyKeys(Keyboard sourceKeys, Keyboard targetKeys)
        {
            sourceKeys.Move.Crouch = targetKeys.Move.Crouch;
            sourceKeys.Move.Down = targetKeys.Move.Down;
            sourceKeys.Move.Jump = targetKeys.Move.Jump;
            sourceKeys.Move.Left = targetKeys.Move.Left;
            sourceKeys.Move.Right = targetKeys.Move.Right;
            sourceKeys.Move.Run = targetKeys.Move.Run;
            sourceKeys.Move.Up = targetKeys.Move.Up;
        }

        public void ApplyKeys(Keyboard targetKeys)
        {
            Move.Crouch = targetKeys.Move.Crouch;
            Move.Down = targetKeys.Move.Down;
            Move.Jump = targetKeys.Move.Jump;
            Move.Left = targetKeys.Move.Left;
            Move.Right = targetKeys.Move.Right;
            Move.Run = targetKeys.Move.Run;
            Move.Up = targetKeys.Move.Up;
        }
    }

    [System.Serializable]
    public struct Move
    {
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Right;
        public KeyCode Left;
        public KeyCode Jump;
        public KeyCode Crouch;
        public KeyCode Run;
    }
}

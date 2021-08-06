using UnityEngine;
using Core.Camera.Effects;
using Core.Camera.Movement.Data;

namespace Core.Camera.Movement
{
    [CreateAssetMenu(fileName = "CameraState", menuName = "ScriptableObject/CameraState", order = 1)]
    public class CameraState : ScriptableObject
    {
        public CameraSettings MainSettings;
        public BobData Bob;
    }
}

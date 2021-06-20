using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Crouch settings.
    /// </summary>
    [Serializable]
    public struct Crouch
    {
        /// <summary>
        /// Height when crouching.
        /// </summary>
        public float HeightWhenCrouching;

        /// <summary>
        /// Speed up when crouching stoped.
        /// </summary>
        [Header("Speeds Crouch")]
        public float SpeedUp;

        /// <summary>
        /// Speed down when crouching start.
        /// </summary>
        public float SpeedDown;

        /// <summary>
        /// Camera lowering distance.
        /// </summary>
        [Space]
        public float CameraLoweringDistance;

        /// <summary>
        /// 
        /// </summary>
        [Header("Raycast")]
        public float DistanceCheckRaycasting;

        /// <summary>
        /// Is check crouch raycast.
        /// </summary>
        [Header("Info")]
        [ReadOnly]
        public bool IsCheckCrouchRaycast;

        /// <summary>
        /// Character normal height.
        /// </summary>
        [ReadOnly]
        public float CharacterNormalHeight;

        public void SaveHeightCharacter(CharacterController character)
        {
            CharacterNormalHeight = character.height;
        }
    }
}

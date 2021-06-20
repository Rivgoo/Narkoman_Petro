using System;
using UnityEngine;

namespace Core.Camera.Movement.Data
{
    /// <summary>
    /// Camera movement settings.
    /// </summary>
    [Serializable]
    public struct Movement
    {
        /// <summary>
        /// Sensityvity x axes.
        /// </summary>
        [Header("Sensitivity")]
        public float XSensitivity;

        /// <summary>
        /// Sensityvity y axes.
        /// </summary>
        public float YSensitivity;

        /// <summary>
        /// Speed transition between max rotation value.
        /// </summary>
        [Header("Speed Transition To Max Value Rotation")]
        public float SpeedTransition;

        /// <summary>
        /// Defoult max angle rotation.
        /// </summary>
        [Space]
        public Vector2Angle DefoultAngle;

        /// <summary>
        /// Slowing rotation.
        /// </summary>
        [Space]
        public float SlowingRotation;

        /// <summary>
        /// Current max angle rotation.
        /// </summary>
        [Header("Info")]
        [ReadOnly]
        public Vector2Angle CurrentAngle;
    }
}

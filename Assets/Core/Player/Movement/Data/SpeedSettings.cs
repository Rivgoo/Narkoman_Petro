using UnityEngine;
using System;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Player Speeds Settings
    /// </summary>
    [Serializable]
    public struct SpeedSettings
    {
        /// <summary>
        /// Speed Transition Between Speeds
        /// </summary>
        public float SpeedTransitionBetweenSpeeds;
    }
}

using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Player gravity settings.
    /// </summary>
    [Serializable]
    public struct Gravity
    {
        /// <summary>
        /// Gravity force.
        /// </summary>
        public float GravityForce;

        /// <summary>
        ///  Distancy to ground when gravity apllying.
        /// </summary>
        [Space]
        public float StickToGroundForce;

        /// <summary>
        /// World gravity.
        /// </summary>
        [Space]
        public Vector3 WorldGravity;

        /// <summary>
        /// World gravity on start a game.
        /// </summary>
        [Header("Info")]
        [ReadOnly]
        public Vector3 OriginalWorldGravity;
    }
}

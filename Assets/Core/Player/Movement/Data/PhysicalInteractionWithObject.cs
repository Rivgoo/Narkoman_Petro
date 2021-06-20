using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Applying force to objects settings.
    /// </summary>
    [Serializable]
    public struct PhysicalInteractionForces
    {
        /// <summary>
        /// Forse when player walking.
        /// </summary>
        public float Walk;

        /// <summary>
        /// Forse when player crouching.
        /// </summary>
        public float Crouch;

        /// <summary>
        /// Forse when player runing.
        /// </summary>
        public float Run;
    }
}

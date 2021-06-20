using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// State of player movement types.
    /// </summary>
    [Serializable]
    public struct MovementStates
    {
        /// <summary>
        /// Current type player movement.
        /// </summary>
        public TypeMovement CurrentTypeMovement;

        /// <summary>
        /// Player jumping.
        /// </summary>
        [ReadOnly]
        public bool Jumping;

        /// <summary>
        /// Player crouching.
        /// </summary>
        [ReadOnly]
        public bool Crouching;

        /// <summary>
        /// Player walking.
        /// </summary>
        [ReadOnly]
        public bool Walking;

        /// <summary>
        /// Player moving.
        /// </summary>
        [ReadOnly]
        public bool Moving;

        /// <summary>
        /// player runing.
        /// </summary>
        [ReadOnly]
        public bool Running;

        /// <summary>
        /// Player risesing.
        /// </summary>
        [ReadOnly]
        public bool Risesing;

        /// <summary>
        /// Previously Grounded.
        /// </summary>
        [ReadOnly]
        public bool PreviouslyGrounded;
    }

    public enum TypeMovement
    {
        None = 0,
        Walk = 1, 
        Run = 2,
        Crouch = 3
    }
}

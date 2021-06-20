using UnityEngine;
using System;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Move player data.
    /// </summary>
    [Serializable]
    public struct Movement
    {
        /// <summary>
        /// Character.
        /// </summary>
        public CharacterController CharacterController;

        /// <summary>
        /// Direction move.
        /// </summary>
        [ReadOnly]
        public Vector3 Direction;

        /// <summary>
        /// User input.
        /// </summary>
        [ReadOnly]
        public Vector2 UserInput;

        /// <summary>
        /// Type collision with player.
        /// </summary>
        [ReadOnly]
        public CollisionFlags Collision;
    }
}

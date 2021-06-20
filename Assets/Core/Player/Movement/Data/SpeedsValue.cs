using UnityEngine;
using System;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Player speed values.
    /// </summary>
    [Serializable]
    public struct SpeedsValue
    {
        /// <summary>
        /// Speed walk.
        /// </summary>
        public float Walk;

        /// <summary>
        /// Speed run.
        /// </summary>
        public float Run;

        /// <summary>
        /// Speed jump.
        /// </summary>
        public float Jump;

        /// <summary>
        /// Speed crouch.
        /// </summary>
        public float Crouch;

        /// <summary>
        /// Current player speed.
        /// </summary>
        [Header("Info")]
        [ReadOnly]
        public float Current;

        /// <summary>
        /// Add value to speeds.
        /// </summary>
        /// <param name="speedsValue">Values of the added speeds.</param>
        public void Add(SpeedsValue speedsValue)
        {
            Jump = ClampValue(Jump, speedsValue.Jump, 10);
            Run = ClampValue(Run, speedsValue.Run, 10);
            Walk = ClampValue(Walk, speedsValue.Walk, 10);
            Crouch = ClampValue(Crouch, speedsValue.Crouch, 10);
        }

        /// <summary>
        /// Subtract values from speeds.
        /// </summary>
        /// <param name="speedsValue">Values of the substructed speeds..</param>
        public void Substruct(SpeedsValue speedsValue)
        {
            Jump = ClampValue(Jump, -speedsValue.Jump, 10);
            Run = ClampValue(Run, -speedsValue.Run, 10);
            Walk = ClampValue(Walk, -speedsValue.Walk, 10);
            Crouch = ClampValue(Crouch, -speedsValue.Crouch, 10);
        }

        private float ClampValue(float firstNumber, float secondNumber, float maxValue)
        {
            return Mathf.Clamp(firstNumber + secondNumber, 0, maxValue);
        }
    }
}

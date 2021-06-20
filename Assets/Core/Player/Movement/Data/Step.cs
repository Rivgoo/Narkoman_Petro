using UnityEngine;
using System;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Player step settings.
    /// </summary>
    [Serializable]
    public struct Step
    {
        /// <summary>
        /// Interval between step.
        /// </summary>
        public float StepInterval;

        /// <summary>
        /// Run step lenghten.
        /// </summary>
        [Range(0, 3f)]
        public float RunStepLenghten;

        /// <summary>
        /// Walk step lenghten.
        /// </summary>
        [Range(0, 3f)]
        public float WalkStepLenghten;

        /// <summary>
        /// Crouch step lenghten.
        /// </summary>
        [Range(0, 3f)]
        public float CrouchStepLenghten;

        /// <summary>
        /// Step cycle.
        /// </summary>
        [Header("Info")]
        [ReadOnly]
        public float StepCycle;

        /// <summary>
        /// Next step.
        /// </summary>
        [ReadOnly]
        public float NextStep;
    }
}

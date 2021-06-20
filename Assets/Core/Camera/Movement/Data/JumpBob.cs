using System;
using UnityEngine;

namespace Core.Camera.Movement.Data
{
    /// <summary>
    /// Animation camera data when jumped.
    /// </summary>
    [Serializable]
    public struct JumpBob
    {
        /// <summary>
        /// Duration animation.
        /// </summary>
        public float BobDuration;

        /// <summary>
        /// Force animation.
        /// </summary>
        public float BobAmount;
    }
}

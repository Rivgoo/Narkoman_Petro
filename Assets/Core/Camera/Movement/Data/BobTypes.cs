using System;
using UnityEngine;

namespace Core.Camera.Movement.Data
{
    /// <summary>
    /// Bob types for camera animation.
    /// </summary>
    [Serializable]
    public struct BobTypes
    {
        /// <summary>
        /// Animation run data.
        /// </summary>
        public BobData Run;

        /// <summary>
        /// Animation walk data.
        /// </summary>
        public BobData Walk;

        /// <summary>
        /// Animation stay data.
        /// </summary>
        public BobData Stay;

        /// <summary>
        /// Animation crouch data.
        /// </summary>
        public BobData Crouch;
    }
}

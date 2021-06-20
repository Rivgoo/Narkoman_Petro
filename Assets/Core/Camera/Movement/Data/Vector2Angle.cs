using System;
using UnityEngine;

namespace Core.Camera.Movement.Data
{
    /// <summary>
    /// Max and min angle.
    /// </summary>
    [Serializable]
    public struct Vector2Angle
    {
        /// <summary>
        /// Minimum angle.
        /// </summary>
        public float Minimum;

        /// <summary>
        /// Maxsimum angle.
        /// </summary>
        public float Maximum;
    }
}

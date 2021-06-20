using System;
using UnityEngine;
using Core.Camera.Effects;

namespace Core.Camera.Movement.Data
{
    /// <summary>
    /// Camera shake settings.
    /// </summary>
    [Serializable]
    public struct ShakeCamera
    {
        /// <summary>
        /// Camera animatior.
        /// </summary>
        public CurveControlledBob HeadBob;

        /// <summary>
        /// Speed animation.
        /// </summary>
        [Space]
        public float Speed;

        public float MaxHeightCamera;
        public float MinHeightCamera;

        public float MaxXShakeCamera;
        public float MaxZShakeCamera;

        [Header("Info")]
        [ReadOnly]
        public Vector3 CrouchVector;
    }
}

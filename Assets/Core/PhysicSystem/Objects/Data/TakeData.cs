using System;
using UnityEngine;

namespace Core.PhysicSystem.Objects.Data
{
    [Serializable]
    public struct TakeData
    {
        [Range(0.1f, 10)]
        public float MaxDistancyToObject;

        [Range(0.1f, 10)]
        public float MaxDistancyKeepingObject;
    }
}

using System;
using UnityEngine;

namespace Core.PhysicSystem.Objects.Data
{
    [Serializable]
    public struct Forces
    {
        [Range(1, 250)]
        public float Throw;

        [Range(1, 100)]
        public float Put;

        [Space]
        [Range(1, 100)]
        public float ForseTorque;
        public Vector3 MaxVectorTorque;

        public Vector3 GetRandomVectorTorque(Vector3 target, Vector3 directionNormalize)
        {
            return new Vector3(UnityEngine.Random.Range(-target.x, target.x) * directionNormalize.x, 
                               UnityEngine.Random.Range(-target.y, target.y) * directionNormalize.y, 
                               UnityEngine.Random.Range(-target.z, target.z) * directionNormalize.z);
        }
    }
}

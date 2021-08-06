using UnityEngine;
using Core.PhysicSystem.Effects;

namespace Core.PhysicSystem.Objects
{
	public abstract class IPhysicObject  : MonoBehaviour
	{	
        /// <summary>
        /// Take object.
        /// </summary>
		public abstract void Take();
	
        /// <summary>
        /// Throw away object.
        /// </summary>
        /// <param name="vector">Throw vector.</param>
        public abstract void PutObject(Vector3 vector);

        /// <summary>
        /// Throw hard object.
        /// </summary>
        /// <param name="vector">Throw vector</param>
        public abstract void ThrowObject(Vector3 vector);
		
        /// <summary>
        /// Move object.
        /// </summary>
        /// <param name="targetPosition">Target position.</param>
        /// <param name="moveSpeed">Player current move speed.</param>
        public abstract void Move(Vector3 targetPosition, float moveSpeed);             	

        /// <summary>
        /// Is can to take the object.
        /// </summary>
        /// <param name="player">Position player.</param>
        /// <returns>Is object active.</returns>
        public abstract bool IsCanTakeObject(Transform player);

        /// <summary>
        /// Is can keeping the object.
        /// </summary>
        /// <param name="player">Position player.</param>
        /// <returns>Is object active.</returns>
        public abstract bool IsCanKeepingObject(Transform player);

        /// <summary>
        /// Get object position.
        /// </summary>
        /// <returns>Object position.</returns>
        public abstract Vector3 GetObjectPosition();

        /// <summary>
        /// Set point collision.
        /// </summary>
        /// <param name="pointCollision">Point collision.</param>
        public abstract void SetPointCollision(Vector3 pointCollision);

        /// <summary>
        /// Get a effects component.
        /// </summary>
        /// <returns>Effects component gorup</returns>
        public abstract EffectsComponentGroup GetEffects();
	}
}

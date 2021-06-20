using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    /// <summary>
    /// Blocking types player movement.
    /// </summary>
    [Serializable]
    public struct BlockingTypeMovements
    {
        /// <summary>
        /// Blocking walk.
        /// </summary>
        public bool Walk;

        /// <summary>
        /// Blocking jump.
        /// </summary>
        public bool Jump;

        /// <summary>
        /// Blocking crouch.
        /// </summary>
        public bool Crouch;

        /// <summary>
        /// Blocking run.
        /// </summary>
        public bool Run;
        
        /// <summary>
        /// Blocking all types movement.
        /// </summary>
        public bool All { get { return CheckAllBlocking(); } set { BlockingAllMovement(value); } }

        private void BlockingAllMovement(bool isBlocking)
        {
            if (isBlocking)
            {
                Jump = true;
                Crouch = true;
                Run = true;
                Walk = true;

            }
            else
            {
                Jump = false;
                Crouch = false;
                Run = false;
                Walk = false;
            }
        }

        private bool CheckAllBlocking()
        {
            return Jump && Crouch && Run && Walk;
        }
    }
}

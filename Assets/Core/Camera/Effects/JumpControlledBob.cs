using System;
using System.Collections;
using UnityEngine;
using Core.Camera.Movement.Data;

namespace Core.Camera.Effects
{
    /// <summary>
    /// Animation a camera for jump.
    /// </summary>
    public class JumpControlledBob
    {
        /// <summary>
        /// Offset Camera
        /// </summary>
        public float Offset { get; private set; }
    	
    	private JumpBob _bob;

        /// <summary>
        /// Play jump animation a camera.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayBobCycle()
        {
            // make the camera move down slightly
            float time = 0f;

            while (time < _bob.BobDuration)
            {
                Offset = Mathf.Lerp(0f, _bob.BobAmount, time / _bob.BobDuration);
                time += Time.deltaTime;
                
                yield return new WaitForFixedUpdate();
            }

            // make it move back to neutral
            time = 0f;
            
            while (time < _bob.BobDuration)
            {
                Offset = Mathf.Lerp(_bob.BobAmount, 0f, time / _bob.BobDuration);
                time += Time.deltaTime;
                
                yield return new WaitForFixedUpdate();
            }

            Offset = 0f;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bob">Jump bob.</param>
        public JumpControlledBob(JumpBob bob)
        {
        	_bob = bob;
        }
    }
}

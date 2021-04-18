using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class LerpControlledBob
    {
      	[SerializeField]private float BobDuration;        
        [SerializeField] private float BobAmount;

        private float _offset;

        /// <summary>
        /// Provides the offset 
        /// </summary>
        /// <returns>Offset value</returns>
        public float Offset()
        {
            return _offset;
        }

        public IEnumerator PlayBobCycle()
        {
            // make the camera move down slightly
            float t = 0f;
            while (t < BobDuration)
            {
                _offset = Mathf.Lerp(0f, BobAmount, t/BobDuration);
                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            // make it move back to neutral
            t = 0f;
            while (t < BobDuration)
            {
                _offset = Mathf.Lerp(BobAmount, 0f, t/BobDuration);
                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            _offset = 0f;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [Serializable]
    [SerializeField]
    internal class LerpControlledBob
    {
    	[SerializeField]
      	private float BobDuration;
        [SerializeField]
        private float BobAmount;

        private float m_Offset = 0f;


        // provides the offset that can be used
        internal float Offset()
        {
            return m_Offset;
        }


        internal IEnumerator DoBobCycle()
        {
            // make the camera move down slightly
            float t = 0f;
            while (t < BobDuration)
            {
                m_Offset = Mathf.Lerp(0f, BobAmount, t/BobDuration);
                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            // make it move back to neutral
            t = 0f;
            while (t < BobDuration)
            {
                m_Offset = Mathf.Lerp(BobAmount, 0f, t/BobDuration);
                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            m_Offset = 0f;
        }
    }
}

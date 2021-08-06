using UnityEngine;

namespace Core.Sound.Object
{
    [RequireComponent(typeof(Collider))]
    public class BlowSoundsPlayer : MonoBehaviour
    {
        [SerializeField]
        private float _minVelocityForPlaySound;

        [Space]
        [SerializeField]
        private AudioSource _sorce;

        [SerializeField]
        private AudioClip[] _sounds;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.relativeVelocity.magnitude > _minVelocityForPlaySound)
            {
                int index = Random.Range(0, _sounds.Length);

                _sorce.clip = _sounds[index];
                _sorce.Play();
            }
        }
    }
}                                                                

using UnityEngine;

namespace Core.Sound.Object
{
    public class BlowSoundsPlayerForDestroyObject : MonoBehaviour
    {
        [SerializeField]
        private float _minVelocityForPlaySound;

        [SerializeField]
        private float _maxVelocityForPlaySound;

        [Space]
        [SerializeField]
        private AudioSource _sorce;

        [SerializeField]
        private AudioClip[] _sounds;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.relativeVelocity.magnitude > _minVelocityForPlaySound &&
               collision.relativeVelocity.magnitude < _maxVelocityForPlaySound)
            {
                int index = Random.Range(0, _sounds.Length);

                _sorce.clip = _sounds[index];
                _sorce.Play();
            }
        }
    }
}

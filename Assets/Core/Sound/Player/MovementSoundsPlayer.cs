using UnityEngine;

namespace Core.Sound.Player
{  [RequireComponent(typeof(Collider))]
    public class MovementSoundsPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _footStepSource;
        [SerializeField]
        private AudioSource _jumpSource;

        [Space]
        [SerializeField]
        [ReadOnly]
        private AudioClip[] _footStepSounds;

        [SerializeField]
        [ReadOnly]
        private AudioClip[] _jumpSounds;

        [SerializeField]
        [ReadOnly]
        private AudioClip[] _landingSounds;

        public void PlayFootStep()
        {
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, _footStepSounds.Length);

            _footStepSource.clip = _footStepSounds[n];
            _footStepSource.PlayOneShot(_footStepSource.clip);

            // move picked sound to index 0 so it's not picked next time
            _footStepSounds[n] = _footStepSounds[0];
            _footStepSounds[0] = _footStepSource.clip;
        }

        public void PlayJump()
        {
            PlayJumpSounds(_jumpSounds);
        }

        public void PlayLanding()
        {
            PlayJumpSounds(_landingSounds);
        }

        public void SetFootStepSounds(AudioClip[] sounds)
        {
            _footStepSounds = sounds;
        }

        public void SetJumpSounds(AudioClip[] jump, AudioClip[] landing)
        {
            _jumpSounds = jump;
            _landingSounds = landing;
        }

        private void PlayJumpSounds(AudioClip[] sounds)
        {
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int index = Random.Range(1, sounds.Length);

            _jumpSource.clip = sounds[index];
            _jumpSource.Play();

            // move picked sound to index 0 so it's not picked next time
            sounds[index] = sounds[0];
            sounds[0] = _jumpSource.clip;
        }
    }
}

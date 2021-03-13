using UnityEngine;
using System;

namespace Player
{
	[Serializable]
    [SerializeField]
	public class PlayerSound
	{
		[Header("Audio Source")]
        [SerializeField] private AudioSource AudioSource;
        
		[Header("Sound")]
        [SerializeField] private AudioClip[] _footstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip _jumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip _landSound;           // the sound played when character touches back on ground.
	
		public void PlayLandingSound()
		{
			AudioSource.clip = _landSound;
			Play();
		}
		
		public void PlayJumpSound()
        {
            AudioSource.clip = _jumpSound;
            Play();
        }
		
		public void PlayFootStepAudio()
        {
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = UnityEngine.Random.Range(1, _footstepSounds.Length);
            AudioSource.clip = _footstepSounds[n];
            AudioSource.PlayOneShot(AudioSource.clip);
            
            // move picked sound to index 0 so it's not picked next time
            _footstepSounds[n] = _footstepSounds[0];
            _footstepSounds[0] = AudioSource.clip;
        }
		
		private void Play()
		{
			AudioSource.Play();
		}
	}

}

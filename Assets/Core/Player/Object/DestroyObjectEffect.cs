using UnityEngine;
using SettingsGame;

namespace Objects
{
	public class DestroyObjectEffect : MonoBehaviour
	{
		[SerializeField] private PhysicDestroyingObjectEffect _effect;
		
		private float _timeLive;
	
		private void Start()
		{
			PlayParticle();
			PlayAudio();
			PlayDestroyObject();
		}
		
		private void Update()
		{
			TimerToDestroyPhysic();
		}
		
		private void PlayAudio()
		{
			_effect.AudioSource.clip = _effect.Clips[Random.Range(0 , _effect.Clips.Length)];
			_effect.AudioSource.Play();
		}
		
		private void PlayParticle()
		{
			for (int i = 0; i < _effect.Particle.Length; i++)
			{	
				_effect.Particle[i].Play(); 
				//_effect.Particle[i].transform.rotation = Quaternion.Euler(0,0,0);
			}
		}
		
		private void PlayDestroyObject()
		{
			for (int i = 0; i < _effect.FragmentsObjects.Length; i++)
			{
				_effect.FragmentsObjects[i].AddForceFragment(_effect.ForceDestroy);
			}
		}
		
		private void DestroyAudio()
		{
			Destroy(_effect.AudioSource);
		}
			
		private void DestroyParticals()
		{
			for (int i = 0; i < _effect.Particle.Length; i++)
			{	
				Destroy(_effect.Particle[i]);
			}
		}
		
		private void TimerToDestroyPhysic()
		{
			if (_timeLive <= SettingsPhysic.Quality.TimeToDestroyPhysic) 
			{
				_timeLive += Time.deltaTime;
				
			}
			else
			{ 	
				if (SettingsPhysic.Quality.IsDestroyPhysic)
				{
					DestroyPhysic();
				}
				DestroyAudio();
				DestroyParticals();
				DestroyOriginalObject();
			}
		}
		
		private void DestroyOriginalObject()
		{
			Destroy(this);
		}
		
		private void DestroyPhysic()
		{
			if (SettingsPhysic.Quality.IsDestroyObject)
			{
				for (int i = 0; i < _effect.FragmentsObjects.Length; i++)
				{
					_effect.FragmentsObjects[i].DestroyObject();
				}
			}
			else if (SettingsPhysic.Quality.IsDestroyCollider)
			{
				for (int i = 0; i < _effect.FragmentsObjects.Length; i++)
				{
					_effect.FragmentsObjects[i].DestroyPhysic();
				}
			}
			else if (SettingsPhysic.Quality.IsDestroyRigidboby) 
			{
				for (int i = 0; i < _effect.FragmentsObjects.Length; i++)
				{
					_effect.FragmentsObjects[i].DestroyRigidboby();  
				}
			}
		}
	}
	
	[System.Serializable]
	public struct PhysicDestroyingObjectEffect
	{
		[Header("Audio")]
		public AudioSource AudioSource;
		public AudioClip[] Clips;
		
		[Header("Particles")]
		public ParticleSystem[] Particle;
		
		[Header("FragmentDestroy")]
		public FragmentDestroyPhysic[] FragmentsObjects;
		
		[Space]
		public float ForceDestroy;
	}
}
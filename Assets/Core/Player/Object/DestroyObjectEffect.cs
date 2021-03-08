using UnityEngine;

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
		
		private void DestroyAudio()
		{
			Destroy(_effect.AudioSource);
		}
		
		private void PlayParticle()
		{
			for (int i = 0; i < _effect.Particle.Length; i++)
			{	
				_effect.Particle[i].Play(); 
				//_effect.Particle[i].transform.rotation = Quaternion.Euler(0,0,0);
			}
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
			if (_timeLive <= _effect.TimeToDestroyPhysic) 
			{
				_timeLive += Time.deltaTime;
				
			}
			else
			{
				DestroyAudio();
				DestroyParticals();
				DestroyPhysic();
				DestroyOriginalObject();
			}
		}
		
		private void DestroyOriginalObject()
		{
			Destroy(this);
		}
		
		private void DestroyPhysic()
		{
			for (int i = 0; i < _effect.FragmentsObject.Length; i++)
			{
				_effect.FragmentsObject[i].DestroyPhysic();
			}
		}
		
		private void PlayDestroyObject()
		{
			for (int i = 0; i < _effect.FragmentsObject.Length; i++)
			{
				_effect.FragmentsObject[i].AddForceFragment(_effect.ForceFlying);
			}
		}
	}
	
	[System.Serializable]
	public struct PhysicDestroyingObjectEffect
	{
		public AudioSource AudioSource;
		public AudioClip[] Clips;
		public ParticleSystem[] Particle;
		public FragmentDestroyPhysic[] FragmentsObject;
		public float TimeToDestroyPhysic;
		public float ForceFlying;
	}
}
using UnityEngine;

public class SoundInteractionObjects : MonoBehaviour 
{
	[SerializeField] private float _minValueForPlaySound;
	[SerializeField] private float _maxValueForPlaySound;
	
	[SerializeField] private AudioClip[] _firstLevelSounds;
	
	[SerializeField] private AudioSource _source;
		
	private void OnCollisionEnter(Collision collision)
	{	
		if (collision.relativeVelocity.magnitude > _minValueForPlaySound && collision.relativeVelocity.magnitude < _maxValueForPlaySound)
		{	
			 PlaySound(_firstLevelSounds);
		}	
		
	}
	
	private void PlaySound(AudioClip[] _clips)
	{
		_source.clip = _clips[Random.Range(0, _clips.Length)];
		_source.Play();
	}
}

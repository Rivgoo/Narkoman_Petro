using UnityEngine;
using System.Collections;

namespace Core.PhysicSystem.Objects
{
    public class Destroyed : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip[] _clips;

        [SerializeField]
        private FragmentsDestroyed[] _fragmentsObjects;

        [Space]
        [SerializeField]
        private float _addedForceToFragments;

        [Space]
        [SerializeField]
        private float _timeToDestroyScriptInSecond;

        private void Start()
        {
            PlayAudio();
            AddForceToFragments();
            StartCoroutine(Timer());
        }

        private void PlayAudio()
        {
            _audioSource.clip = _clips[Random.Range(0, _clips.Length)];
            _audioSource.Play();
        }

        private void AddForceToFragments()
        {
            for (int i = 0; i < _fragmentsObjects.Length; i++)
            {
                _fragmentsObjects[i].AddForceFragment(_addedForceToFragments);
            }
        }

        private void DestroyAudio()
        {
            Destroy(_audioSource);
        }

        private void DestroyScript()
        {
            Destroy(this);
        }

        private void DestroyFragmentsScript()
        {
            for (int i = 0; i < _fragmentsObjects.Length; i++)
            {
                _fragmentsObjects[i].DestroyScript();
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_timeToDestroyScriptInSecond);

            DestroyAudio();
            DestroyFragmentsScript();
            DestroyScript();
        }
    }
}

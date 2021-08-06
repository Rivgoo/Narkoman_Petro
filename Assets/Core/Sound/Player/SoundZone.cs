using UnityEngine;

namespace Core.Sound.Player
{
    [RequireComponent(typeof(Collider))]
    public abstract class SoundZone  : MonoBehaviour
    {
        [SerializeField]
        protected AudioClip[] _sounds;

        protected abstract void SetSound(MovementSoundsPlayer player);

        private void OnTriggerEnter(Collider other)
        {
            MovementSoundsPlayer player = other.gameObject.GetComponent<MovementSoundsPlayer>() as MovementSoundsPlayer;

            if(player != null) 
            {
                SetSound(player);
            }
        }

        private void Start()
        {
            CheckCountSounds();
        }

        private void CheckCountSounds()
        {
            if(_sounds.Length < 1)
            {
                throw new System.ArgumentOutOfRangeException("Sounds", "The number of sounds must be more than 2!");
            }
        }
    }
}

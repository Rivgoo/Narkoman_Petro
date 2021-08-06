using UnityEngine;

namespace Core.Sound.Player
{
    public class JumpZone : SoundZone
    {
        [Space]
        [SerializeField]
        private AudioClip[] _soundsLanding;

        protected override void SetSound(MovementSoundsPlayer player)
        {
            player.SetJumpSounds(_sounds, _soundsLanding);
        }
    }
}

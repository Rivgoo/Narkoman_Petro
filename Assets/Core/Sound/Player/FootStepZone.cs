using UnityEngine;

namespace Core.Sound.Player
{
    public class FootStepZone  : SoundZone
    {
        protected override void SetSound(MovementSoundsPlayer player)
        {
            player.SetFootStepSounds(_sounds);
        }
    }
}

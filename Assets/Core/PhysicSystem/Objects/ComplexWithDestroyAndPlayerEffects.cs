using UnityEngine;
using Core.Camera.Movement;
using Core.Player.Characteristics;
using Core.Player.Movement.Data;

namespace Core.PhysicSystem.Objects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("PhysicObjects/ComplexWithDestroyAndPlayerEffects")]
    public class ComplexWithDestroyAndPlayerEffects : ComplexWithDestroy
    {
        [Space]
        [SerializeField]
        private PlayerEffects _playerEffects;

        public override void UpdateEffectsWhenMove(EndurancePlayer enduracne)
        {
            enduracne.SubstructEnduranceOfKeepObject(_playerEffects.EnduranceSubstructData);
        }
    }
}

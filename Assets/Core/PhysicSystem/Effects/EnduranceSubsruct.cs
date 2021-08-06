using UnityEngine;
using Core.Player.Characteristics;

namespace Core.PhysicSystem.Effects
{
    [AddComponentMenu("PhysicObjectEffects/EnduranceSubsruct")]
    public class EnduranceSubsruct : MonoBehaviour
    {
        public EndurancePlayer Endurance
        {
            set
            {
                _enduracne = value;
            }
        }

        [SerializeField]
        private ObjectEnduranceData _EnduranceSubstructData;

        private EndurancePlayer _enduracne;

        public void Substruct()
        {
            _enduracne.SubstructEnduranceOfKeepObject(_EnduranceSubstructData);
        }
    }
}

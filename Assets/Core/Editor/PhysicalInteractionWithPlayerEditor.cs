using UnityEditor;
using UnityEngine;
using Core.Player.Physic;

namespace Core
{
    [CustomEditor(typeof(PhysicalInteractionWithPlayer))]
    public class PhysicalInteractionWithPlayerEditor : Editor
    {
        private PhysicalInteractionWithPlayer _interaction;

        private void OnEnable()
        {
            _interaction = (PhysicalInteractionWithPlayer)target;
        }

        public override void OnInspectorGUI()
        {
            //Very bad, because I don't know how to do it
            base.DrawDefaultInspector();
            _interaction.UpdateForces(); 
        }
    }
}

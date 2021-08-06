using Core.GameUI.Text;
using UnityEditor;
using UnityEngine;

namespace Core.GameUI.Editor
{
    [CustomEditor(typeof(LetterGroup))]
    public class LetterGroupEditor : UnityEditor.Editor
    {
        private LetterGroup _letterSpacing;

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            if(GUILayout.Button("Create Letter Objects"))
            {
                _letterSpacing = (LetterGroup)target;
                _letterSpacing.CreateLetterObjects();
            }

            if(GUILayout.Button("Delete Letters"))
            {
                _letterSpacing = (LetterGroup)target;
                _letterSpacing.DeleteLettersEditor();
            }
        }
    }
}

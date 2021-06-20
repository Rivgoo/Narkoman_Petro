using UnityEngine;

namespace Core.Game
{
    public class QuitGame : MonoBehaviour
    {
        public void Quit()
        {
            //If we are running in the editor
	        #if UNITY_EDITOR
		        UnityEditor.EditorApplication.isPlaying = false;
	        #endif

            Application.Quit();
        }
    }
}

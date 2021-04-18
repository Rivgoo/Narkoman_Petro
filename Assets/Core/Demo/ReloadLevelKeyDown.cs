using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelKeyDown : MonoBehaviour 
{
	[SerializeField] private int _numberScene;
	
	
	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			SceneManager.LoadScene(_numberScene);
			//SceneManager.UnloadScene();
		}
	}
}

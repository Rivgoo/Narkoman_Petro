using UnityEngine;

internal class GameOver : MonoBehaviour
{
	[Header("Panel Game Over")]
	[SerializeField]
	private ShowGameOver _panelGameOver;
	
	internal void Activate()
	{
		_panelGameOver.Show();
	}
	
}

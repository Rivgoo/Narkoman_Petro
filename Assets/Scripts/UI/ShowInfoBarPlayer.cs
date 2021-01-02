using UnityEngine;
using UnityEngine.UI;

internal class ShowInfoBarPlayer : MonoBehaviour
{
	[SerializeField]
	private Image _bar;
	
	internal void UpdateBar(float value)
	{
		_bar.fillAmount = value;
	}
}

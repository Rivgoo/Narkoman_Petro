using UnityEngine;
using UnityEngine.UI;

public class UIMainTimer : MonoBehaviour
{
	[SerializeField] private Text _timer;
	[SerializeField] [ReadOnly] private string _textForm;
	
	public void SetTextTimer(float seconds)
	{
		var minutes = (int)(seconds/60);
		var sec = (int)(seconds - (minutes *60));
		
		if (sec == 60 || sec == 0)
		{
			_textForm = minutes + ":" + "00";
		}
		else if(sec < 10)
		{
			_textForm = minutes + ":" + "0" + sec;
		}
		else
		{
			_textForm = minutes + ":" + sec;
		}
		
		_timer.text = _textForm;
	}
	
}

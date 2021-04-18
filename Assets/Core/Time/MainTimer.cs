using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTimer : MonoBehaviour 
{
	[SerializeField] private UIMainTimer _uiTimer;
	[SerializeField] private bool _activeTimer;
	[SerializeField] private float _timeToGameOver;
	
	public void ActivateTimer()
	{
		_activeTimer = true;
		StartCoroutine(Timer());
	}
	
	private void ShowValueTimer()
	{
		_uiTimer.SetTextTimer(_timeToGameOver);
	}
	
	private void CheckGameOver()
	{
		if (_timeToGameOver <= 0.1) 
		{
			_activeTimer = false;
		}
	}
	
	IEnumerator Timer()
	{
		while (_activeTimer)
		{
			_timeToGameOver --;
			CheckGameOver();
			ShowValueTimer();
			yield return new WaitForSeconds(1);
		}
	}
	
	private void Start()
	{
		ActivateTimer();
	}
}

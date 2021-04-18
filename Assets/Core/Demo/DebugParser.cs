using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerData;
using Objects;
using UnityEngine.UI;

public class DebugParser : MonoBehaviour 
{
	[SerializeField] private Text _text;
	
	[SerializeField] private string _t;
	
	public void UpdateInfo(CameraMove _move, PlayerSpeeds _speeds)
	{
		_t = "MaximumX: " + _move.CurrentAngle.Maximum + 
			"\n" + 
			"MinimumX: " + _move.CurrentAngle.Minimum +
			"\n" +
			 "Crouch Speed: " + _speeds.Crouch + 
			"\n" + 
			"Jump Speed: " + _speeds.Jump +
			"\n" +
			 "Run Speed: " + _speeds.Run + 
			"\n" + 
			"Walk Speed: " + _speeds.Walk +
			"\n" + 
			"Current Speed: " + _speeds.Current;
		
		_text.text = _t;
	}
	
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

    public class FPSCounter : MonoBehaviour
    {
    	//[SerializeField] private bool _countPhysicFPS;
    	[SerializeField] private Text _text;
    	
    	[SerializeField] private float _fpsMeasurePeriod = 0.5f;
        
        //private float _fpsNextPeriod;
        
        private float _currentFps;
        
        private const string _displayForm = "{0} FPS";

//        private void Update()
//        {  
//			if (_countPhysicFPS) 
//			{
//				if (Time.fixedUnscaledTime > _fpsNextPeriod)
//	            {
//	            	_currentFps = (int)(1f / Time.fixedUnscaledDeltaTime);
//	
//	                _fpsNextPeriod = Time.fixedUnscaledTime + _fpsMeasurePeriod;
//	                
//	                _text.text = string.Format(_displayForm, _currentFps);
//	            }
//			}  
//        	
//			else
//			{
//	            if (Time.unscaledTime > _fpsNextPeriod)
//	            {
//	            	_currentFps = (int)(1f / Time.unscaledDeltaTime);
//	
//	                _fpsNextPeriod = Time.unscaledTime + _fpsMeasurePeriod;
//	                
//	                _text.text = string.Format(_displayForm, _currentFps);
//	            }
//	        }
//        }
        
	    IEnumerator Start ()
		{
			while (true) 
			{
				yield return new WaitForSeconds (_fpsMeasurePeriod);
				_currentFps = (1 / Time.deltaTime);
				_text.text =  string.Format(_displayForm, Mathf.Round (_currentFps));
			}
		}
   	}


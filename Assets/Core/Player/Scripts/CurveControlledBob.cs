using System;
using UnityEngine;


namespace Player
{
    [Serializable]
    public class CurveControlledBob
    {
    	[Header("Bobs")]
    	[SerializeField] private BobData _run;
    	[SerializeField] private BobData _walk;
    	[SerializeField] private BobData _stay;
    	[SerializeField] private BobData _crouch;
        
        private BobData _currentBob;

        private float _cyclePositionX;
        private float _cyclePositionY;
        private float _cyclePositionZ;
        
        private float _bobBaseInterval;
        private float _time;
        
        private Vector3 _originalCameraPosition;

        public void Setup(Transform camera, float bobBaseInterval)
        {
        	_currentBob = _stay;
            _bobBaseInterval = bobBaseInterval;
            _originalCameraPosition = camera.localPosition;
        }
		
        public void SetTypeBob(TypeBob type)
        {
        	if (type == TypeBob.Stay)
        	{
        		_currentBob = _stay;
        	}
        	else if (type == TypeBob.Walk)
        	{
        		_currentBob = _walk;
        	}
        	else if (type == TypeBob.Run)
        	{
        		_currentBob = _run;
        	}
        	else if (type == TypeBob.Crouch)
        	{
        		_currentBob = _crouch;
        	}
        	
        	_time = _currentBob.AnimatinCurve[_currentBob.AnimatinCurve.length - 1].time;
        }

        public Vector3 DoHeadBob(float speed)
        {
        	float xPos = _originalCameraPosition.x + (_currentBob.AnimatinCurve.Evaluate(_cyclePositionX) * _currentBob.BobRangeX);
        	float yPos = _originalCameraPosition.y + (_currentBob.AnimatinCurve.Evaluate(_cyclePositionY) * _currentBob.BobRangeY);
        	float zPos = _originalCameraPosition.z + (_currentBob.AnimatinCurve.Evaluate(_cyclePositionZ) * _currentBob.BobRangeZ);
			
        	var cyclePosition = (speed * Time.fixedDeltaTime) / _bobBaseInterval;
            _cyclePositionX += cyclePosition * _currentBob.AccelerationX;
            _cyclePositionY += cyclePosition * _currentBob.AccelerationY;
			_cyclePositionZ += cyclePosition * _currentBob.AccelerationZ;            

            if (_cyclePositionX > _time)
            {
                _cyclePositionX -= _time;
            }
            if (_cyclePositionY > _time)
            {
                _cyclePositionY -= _time;
            }
            if (_cyclePositionZ > _time)
            {
                _cyclePositionZ -= _time;
            }
            
            return new Vector3(xPos, yPos, zPos);
        }
    }
    
    public enum TypeBob
    {
    	Stay, Run, Walk, Crouch
    }
    
    [System.Serializable]
    public struct BobData
    {
    	[Header("Ranges")]
    	public float BobRangeX;
        public float BobRangeY;
        public float BobRangeZ;
        
        [Header("Accelerations")]
        [Range(1, 50)] public float AccelerationX;
        [Range(1, 50)] public float AccelerationY;
        [Range(1, 50)] public float AccelerationZ;
        
        [Header("Animation Curve")]
        public AnimationCurve AnimatinCurve;
    }
}


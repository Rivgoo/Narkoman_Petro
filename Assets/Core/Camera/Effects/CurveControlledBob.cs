using System;
using UnityEngine;
using Core.Camera.Movement;
using Core.Camera.Movement.Data;
using Core.Player.Movement.Data;

namespace Core.Camera.Effects
{
    /// <summary>
    /// Anaimation a camera with a bobs.
    /// </summary>
    public class CurveControlledBob
    {
    	private BobTypes _bobs;
        
    	private BobData _currentBob;

        private float _cyclePositionX;
        private float _cyclePositionY;
        private float _cyclePositionZ;
        
        private float _bobBaseInterval;
        private float _time;
        
        private Vector3 _originalCameraPosition;

        private BobData[] _allBobs = new BobData[4];
		
        /// <summary>
        /// Set initial values.
        /// </summary>
        /// <param name="camera">Start camera position.</param>
        /// <param name="bobBaseInterval">Bob case interval.</param>
        /// /// <param name="bobs">Bob types.</param>
        public void Setup(Transform camera, float bobBaseInterval, BobTypes bobs)
        {
        	_bobs = bobs;
        	_currentBob = _bobs.Stay;
            _bobBaseInterval = bobBaseInterval;
            _originalCameraPosition = camera.localPosition;

            UpdateBobs();
        }
		
        /// <summary>
        /// Set current type bob.
        /// </summary>
        /// <param name="type">Type movement.</param>
        public void SetTypeBob(TypeMovement type)
        {
            //UpdateBobs();

        	_currentBob = _allBobs[(int)type];

        	_time = _currentBob.AnimatinCurve[_currentBob.AnimatinCurve.length - 1].time;
        }
		
        /// <summary>
        /// Play animation move the camera.
        /// </summary>
        /// <param name="speed">Speed playing.</param>
        /// <returns>Next Posotin Camera</returns>
        public Vector3 PlayHeadBob(float speed)
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

        private void UpdateBobs()
        {
            _allBobs[0] = _bobs.Stay;
            _allBobs[1] = _bobs.Walk;
            _allBobs[2] = _bobs.Run;
            _allBobs[3] = _bobs.Crouch;
        }
    }
}
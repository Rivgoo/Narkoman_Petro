using System;
using UnityEngine;


namespace Player
{
    [Serializable]
    [SerializeField]
    internal class CurveControlledBob
    {
        [SerializeField] internal float _horizontalBobRange = 0.33f;
        [SerializeField] internal float _verticalBobRange = 0.33f;
        [SerializeField] internal float _bobRange;
        
        [SerializeField]
        private AnimationCurve _bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                            new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                            new Keyframe(2f, 0f)); // sin curve for head bob
        [SerializeField]
        private float VerticaltoHorizontalRatio = 1f;

        private float _cyclePositionX;
        private float _cyclePositionY;
        private float _bobBaseInterval;
        private Vector3 _originalCameraPosition;
        private float _time;


        internal void Setup(Transform camera, float bobBaseInterval)
        {
            _bobBaseInterval = bobBaseInterval;
            _originalCameraPosition = camera.localPosition;

            // get the length of the curve in time
            _time = _bobcurve[_bobcurve.length - 1].time;
        }


        internal Vector3 DoHeadBob(float speed)
        {
        	float xPos = _originalCameraPosition.x + (_bobcurve.Evaluate(_cyclePositionX) * (_horizontalBobRange + _bobRange));
        	float yPos = _originalCameraPosition.y + (_bobcurve.Evaluate(_cyclePositionY) * (_verticalBobRange + _bobRange));

            _cyclePositionX += (speed*Time.deltaTime) / _bobBaseInterval;
            _cyclePositionY += ((speed*Time.deltaTime) / _bobBaseInterval) * VerticaltoHorizontalRatio;

            if (_cyclePositionX > _time)
            {
                _cyclePositionX -= _time;
            }
            if (_cyclePositionY > _time)
            {
                _cyclePositionY -= _time;
            }

            return new Vector3(xPos, yPos, 0f);
        }
    }
}

using Core.Camera.Movement;
using Core.Camera.Movement.Data;
using System;
using UnityEngine;

namespace Core.PhysicSystem.Objects.Data
{
    [Serializable]
    public struct CameraRestrictions
    {
        public Vector2Angle ViewingLimits;

        private bool IsCancelRestrictions;

        public void ApplyRestrictions()
        {
            if(!IsCancelRestrictions)
            {
                MouseLook.AddOffsetValueRatation(ViewingLimits);
                IsCancelRestrictions = true;
            }
        }

        public void CancelRestrictions()
        {
            if(IsCancelRestrictions)
            {
                MouseLook.SubtractOffsetValueRatation(ViewingLimits);
                IsCancelRestrictions = false;
            }
        }
    }
}

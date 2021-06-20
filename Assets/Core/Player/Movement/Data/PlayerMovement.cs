using System;
using UnityEngine;

namespace Core.Player.Movement.Data
{
    public class PlayerMovement : MonoBehaviour
    {
        public Crouch Crouch;
        public SpeedsValue SpeedsValue;
        public SpeedSettings SpeedsSettings;
        public Step Step;
        public Movement Movement;

        public void Start()
        {
            Step.StepCycle = 0f;
            Step.NextStep = Step.StepCycle / 2f;
            Crouch.SaveHeightCharacter(Movement.CharacterController);
        }
    }

    public static class CalculationNumber
    {
        public static float GetNumber(float originalValue, float targetPercent)
        {
            return (originalValue * targetPercent) / 100;
        }
    }
}

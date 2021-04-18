using UnityEngine;
using PlayerData;

namespace PlayerData
{
	public class EditPlayerMovementData
	{
		private MovementPlayerData _editData;
		
		private Vector3 _maxValueDirection = new Vector3(10, 15, 10);
		
		public void Init(MovementPlayerData editData)
		{
			_editData = editData;
		}
		
		public void AddSpeeds(PlayerSpeeds targetSpeeds)
		{
			_editData.Speeds.Jump = Clamp(_editData.Speeds.Jump, targetSpeeds.Jump, 10);
			_editData.Speeds.Run = Clamp(_editData.Speeds.Run, targetSpeeds.Run, 8);
			_editData.Speeds.Walk = Clamp(_editData.Speeds.Walk, targetSpeeds.Walk, 8);
			_editData.Speeds.Crouch = Clamp(_editData.Speeds.Crouch, targetSpeeds.Crouch, 5);
		}
		
		public void SubstructSpeeds(PlayerSpeeds targetSpeeds)
		{
			_editData.Speeds.Jump = Clamp(_editData.Speeds.Jump, -targetSpeeds.Jump, 10);
			_editData.Speeds.Run = Clamp(_editData.Speeds.Run, -targetSpeeds.Run, 8);
			_editData.Speeds.Walk = Clamp(_editData.Speeds.Walk, -targetSpeeds.Walk, 8);
			_editData.Speeds.Crouch = Clamp(_editData.Speeds.Crouch, -targetSpeeds.Crouch, 5);
		}
		
		public void AddPhysicObjects(PhysicObjects targetPhysicObjects)
		{
			_editData.Physic.ForceCrouch = Clamp(_editData.Physic.ForceCrouch, targetPhysicObjects.ForceCrouch, 100);
			_editData.Physic.ForceRun = Clamp(_editData.Physic.ForceRun, targetPhysicObjects.ForceRun, 300);
			_editData.Physic.ForceWalk = Clamp(_editData.Physic.ForceWalk, targetPhysicObjects.ForceWalk, 200);
			_editData.Physic.ForceSmallObject = Clamp(_editData.Physic.ForceSmallObject, targetPhysicObjects.ForceSmallObject, 100);
		}
		
		public void SubstuctPhysicObjects(PhysicObjects targetPhysicObjects)
		{
			_editData.Physic.ForceCrouch = Clamp(_editData.Physic.ForceCrouch, -targetPhysicObjects.ForceCrouch, 100);
			_editData.Physic.ForceRun = Clamp(_editData.Physic.ForceRun, -targetPhysicObjects.ForceRun, 300);
			_editData.Physic.ForceWalk = Clamp(_editData.Physic.ForceWalk, -targetPhysicObjects.ForceWalk, 200);
			_editData.Physic.ForceSmallObject = Clamp(_editData.Physic.ForceSmallObject, -targetPhysicObjects.ForceSmallObject, 100);
		}
		
		public void AddDirection(Vector3 targetDirection)
		{
			_editData.Move.Direction = ClampVector3(_editData.Move.Direction, targetDirection, _maxValueDirection);
		}
		
		public void SubstructDirection(Vector3 targetDirection)
		{
			_editData.Move.Direction = ClampVector3(_editData.Move.Direction, targetDirection * -1, _maxValueDirection);
		}
		
		private float Clamp(float firstNumber, float secondNumber, float maxValue)
		{
			return  Mathf.Clamp(firstNumber + secondNumber, 0, maxValue);
		}
		
		private Vector3 ClampVector3(Vector3 firstVector, Vector3 secondVector, Vector3 maxValue)
		{
			return  new Vector3(Clamp(firstVector.x, secondVector.x, maxValue.x), Clamp(firstVector.y, secondVector.y, maxValue.y), Clamp(firstVector.z, secondVector.z, maxValue.z));
		}
		
	}
}

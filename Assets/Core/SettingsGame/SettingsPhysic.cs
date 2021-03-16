using UnityEngine;

namespace SettingsGame
{
	public class SettingsPhysic : MonoBehaviour
	{
		public static PhysicSettingsData Quality;
		
		[Header("Quality Physic")]
		[SerializeField] public QualityPhysic _typePhysic;
		
		[Header("Quality Very Height")]
		[SerializeField] private PhysicSettingsData _veryHeight;
		
		[Header("Quality Height")]
		[SerializeField] private PhysicSettingsData _height;
		
		[Header("Quality Medium")]
		[SerializeField] private PhysicSettingsData _medium;
		
		[Header("Quality Low")]
		[SerializeField] private PhysicSettingsData _low;
		
		public void SetTypePhysic(QualityPhysic quality)
		{
			_typePhysic = quality;
		}
		
		private void SetQualityPhysic()
		{
			if (_typePhysic == QualityPhysic.VeryHeight)
			{
				Quality = _veryHeight;
			}
			else if (_typePhysic == QualityPhysic.Height)
			{
				Quality = _height;
			}
			else if (_typePhysic == QualityPhysic.Medium)
			{
				Quality = _medium;
			}
			else
			{
				Quality = _low;
			}
			
			UpdateValueWorldPhysic();
			
		}
		
		private void Start()
		{
			SetQualityPhysic();
		}
		
		private void UpdateValueWorldPhysic()
		{
			Physics.defaultSolverIterations = Quality.SolverIterations;
			Physics.defaultSolverVelocityIterations = Quality.SolverVelocityIteration;
		}
	}
	
	public enum QualityPhysic
	{
		VeryHeight, Height, Medium, Low
	}
	
	public enum QualityDestroyObject
	{
		Hight, Medium
	}
	
	[System.Serializable]
	public struct PhysicSettingsData
	{
		[Header("World Physic Settings")]
		public int SolverIterations;
		public int SolverVelocityIteration;
		
		[Header("Destroyed Objects")]
		public bool IsDestroyCollider;
		public bool IsDestroyRigidboby;
		public bool IsDestroyObject;
		
		[Header("Destroy Object Effect")]
		public bool IsDestroyPhysic;
		public QualityDestroyObject QualityDestroyObject;
		public float TimeToDestroyPhysic;
	}
}


using UnityEngine;
using PlayerData;

namespace Objects
{
	public class PhysicObjectDefoult : MonoBehaviour, IPhysicObject 
	{
		[Header("Settings")] 
		[SerializeField] protected DefoultPhysicObjectData _dataObject;

		
		public DefoultPhysicObjectData DataObject { get { return _dataObject; } set { _dataObject = value; }}

	}
}

using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey; 
using Random = UnityEngine.Random;
using PlayerData;
using Objects;

namespace Player
{
	public class MainPlayer : MonoBehaviour
	{	
		[Header("Player Move Settings")]
    	[SerializeField] private MovementPlayerData _defoultMovementPlayer = new MovementPlayerData();
    	
    	[Header("Camera Settings")]
    	[SerializeField] private MovementCameraData _defoultMovementCamera = new MovementCameraData();
    	
    	[Header("Player Sound")]
		[SerializeField] private PlayerSound _sound;
    	
    	[Header("Player Move")]
		[SerializeField] private PlayerMoveController _move;
		
		[Header("Camera Move")]
		[SerializeField] private CameraMoveController _cameraMove;
		
		[Header("Endurance Player")]
		[SerializeField] private EndurancePlayer _endurance;
		
		[Header("Take Object")]
		[SerializeField] private TakeObject _takeObject;

		
		[Header("Player Steps")]
		[SerializeField] private PlayerSteps _steps;
		
		[Header("Physics Interaction Small Object")]
		[SerializeField] private PhysicsInteractionSmallObject _smallObjects;
		
		private MouseLook _mouseLook = new MouseLook();
				
		private MovementPlayerData _editMovementPlayer = new MovementPlayerData();
    	private MovementCameraData _editMovementCamera = new MovementCameraData(); 	
    	
		private void InitScripts()
		{
			InitPlayerData();
			InitCameraData();
			
			// When you Init() then ADD to InitData inizialszation
			_mouseLook.Init(transform , _editMovementCamera); 
			
			_move.Init(_editMovementPlayer, _editMovementCamera, _sound, _endurance);
			
			_cameraMove.Init(_editMovementPlayer, _editMovementCamera, _mouseLook);
			
			_steps.Init(_editMovementPlayer, _sound);
			
			_smallObjects.Init(_editMovementPlayer);
			
			_endurance.Init(_editMovementPlayer);
			
			_takeObject.Init(_mouseLook, _endurance, _editMovementPlayer, _defoultMovementPlayer);
		}
		
		private void InitPlayerData()
		{
			_editMovementPlayer.CharacterController = _defoultMovementPlayer.CharacterController;
			_editMovementPlayer.Crouch = _defoultMovementPlayer.Crouch;
			_editMovementPlayer.Gravity = _defoultMovementPlayer.Gravity;
			_editMovementPlayer.Move = _defoultMovementPlayer.Move;
			_editMovementPlayer.Physic = _defoultMovementPlayer.Physic;
			_editMovementPlayer.Speeds = _defoultMovementPlayer.Speeds;
			_editMovementPlayer.State = _defoultMovementPlayer.State;
			_editMovementPlayer.SpeedsSettings = _defoultMovementPlayer.SpeedsSettings;
			_editMovementPlayer.Step = _defoultMovementPlayer.Step;
		}
		
		private void InitCameraData()
		{
			_editMovementCamera.Camera = _defoultMovementCamera.Camera;
			_editMovementCamera.JumpShake = _defoultMovementCamera.JumpShake;
			_editMovementCamera.OriginalCameraPosition = _defoultMovementCamera.OriginalCameraPosition;
			_editMovementCamera.Shake = _defoultMovementCamera.Shake;
			_editMovementCamera.CameraMove = _defoultMovementCamera.CameraMove;
		}
		
		private void Awake()
		{
			InitScripts();
		}
	}
}
using UnityEngine;
using Keys = PlayerInput.InputKeys.CheckMovementKey; 
using Random = UnityEngine.Random;
using PlayerData;
using Objects;

namespace Player
{
	public class MainPlayer : MonoBehaviour
	{	
		[SerializeField] private DebugParser _debug;
		
		[Space]
    	[SerializeField] private MovementPlayerData _defoultMovementPlayer = new MovementPlayerData();
    	[Space]
    	[SerializeField] private MovementCameraData _defoultMovementCamera = new MovementCameraData();
    	[Space]
		[SerializeField] private PlayerSound _sound;
    	
		[Header("Scripts")]
		[SerializeField] private PlayerMoveController _move;	
		[SerializeField] private CameraMoveController _cameraMove;	
		[SerializeField] private EndurancePlayer _endurance;
		[SerializeField] private TakeObject _takeObject;	
		[SerializeField] private PlayerSteps _playerSteps;
		[SerializeField] private PhysicsInteractionSmallObject _smallObjects;
		
		private MouseLook _mouseLook = new MouseLook();
		private EditPlayerMovementData _editPlayerMovementData = new EditPlayerMovementData();
				
		private MovementPlayerData _editMovementPlayer = new MovementPlayerData();
    	private MovementCameraData _editMovementCamera = new MovementCameraData(); 	
    	
		private void InitScripts()
		{
			InitPlayerData();
			InitCameraData();
			
			
			// When you Init() then ADD to InitData inizialszation
			_mouseLook.Init(transform , _editMovementCamera); 
			_editPlayerMovementData.Init(_editMovementPlayer);
			
			_move.Init(_editMovementPlayer, _editMovementCamera, _sound, _endurance);
			
			_cameraMove.Init(_editMovementPlayer, _editMovementCamera, _mouseLook);
			
			_playerSteps.Init(_editMovementPlayer, _sound);
			
			_smallObjects.Init(_editMovementPlayer);
			
			_endurance.Init(_editMovementPlayer);
			
			_takeObject.Init(_mouseLook, _endurance, _editPlayerMovementData, _editMovementPlayer);
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
			_editMovementPlayer.BlockMovement = _defoultMovementPlayer.BlockMovement;
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
		
		private void Update()
		{
			_debug.UpdateInfo(_editMovementCamera.CameraMove, _editMovementPlayer.Speeds);
		}
	}
}
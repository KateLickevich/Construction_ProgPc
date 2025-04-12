using Construction;
using Player;
using UI;
using UnityEngine;

namespace GameCore
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlacementController _placementController;
        [SerializeField] private UIController _uiController;
        
        private GameModeController _gameModeController;
    
        private void Awake()
        {
            _gameModeController = new GameModeController();
            _gameModeController.SetGameMode(GameModeType.Game);
            
            _placementController.OnGrab += EnterConstructionMode;
        }

        private void Update()
        {
            switch (_gameModeController.CurrentGameModeType)
            {
                case GameModeType.Game:
                    _placementController.CheckFront();
                    break;
                case GameModeType.Construction:
                    _placementController.TryPlacementObject();
                    break;
            }
        }

        private void EnterConstructionMode(Transform placementObject)
        {
            _gameModeController.SetGameMode(GameModeType.Construction);
            _playerController.TakePlacedObject(placementObject);
        }
    }
}
using UnityEngine;

namespace GameCore
{
    public class GameModeController
    {
        private GameModeType _currentGameModeType;

        public GameModeType CurrentGameModeType => _currentGameModeType;

        public void SetGameMode(GameModeType gameModeType)
        {
            _currentGameModeType = gameModeType;

            switch (gameModeType)
            {
                case GameModeType.Game:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameModeType.Construction:
                    break;
            }
        }
    }
}
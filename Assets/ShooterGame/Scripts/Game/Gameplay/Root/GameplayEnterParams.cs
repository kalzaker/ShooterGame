using ShooterGame.Scripts.Game.GameRoot;

namespace ShooterGame.Scripts.Game.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }

        public GameplayEnterParams(string saveFileName, int levelNumber) : base(ScenesNames.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}
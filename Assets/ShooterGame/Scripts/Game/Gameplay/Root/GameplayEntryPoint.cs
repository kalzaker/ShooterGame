using R3;
using ShooterGame.Scripts.DI;
using ShooterGame.Scripts.Game.Gameplay.Root.View;
using ShooterGame.Scripts.Game.GameRoot;
using ShooterGame.Scripts.Game.MainMenu.Root;
using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            GameplayRegistrations.Register(gameplayContainer, enterParams);
            var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);
            
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubj); 
            
            Debug.Log($"GAMEPLAY ENTRY POINT: Run gameplay scene. save file name: {enterParams.SaveFileName}, level to load: {enterParams.LevelNumber}");

            var mainMenuEnterParams = new MainMenuEnterParams("Score");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            
            return exitToMainMenuSceneSignal;
        }
    }
}
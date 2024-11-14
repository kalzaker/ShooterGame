using System.Collections;
using R3;
using ShooterGame.Scripts.Game.Gameplay.Root;
using ShooterGame.Scripts.Game.MainMenu.Root;
using ShooterGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterGame.Scripts.Game.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
            
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == ScenesNames.GAMEPLAY)
            {
                var enterParams = new GameplayEnterParams("ddd.save", 1);
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
                return;
            }

            if (sceneName == ScenesNames.MAIN_MENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            }

            if (sceneName != ScenesNames.BOOT)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesNames.BOOT);
            yield return LoadScene(ScenesNames.GAMEPLAY);

            yield return new WaitForSeconds(1);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(gameplayExitParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
            });
            
            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesNames.BOOT);
            yield return LoadScene(ScenesNames.MAIN_MENU);

            yield return new WaitForSeconds(1);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == ScenesNames.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });
            
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
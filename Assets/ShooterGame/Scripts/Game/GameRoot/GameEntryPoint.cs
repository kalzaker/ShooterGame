using System.Collections;
using ShooterGame.Scripts.Game.Gameplay.Root;
using ShooterGame.Scripts.Game.MainMenu.Root;
using UnityEngine;
using ShooterGame.Scripts.Utils;
using UnityEngine.SceneManagement;

namespace ShooterGame.Scripts
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
                _coroutines.StartCoroutine(LoadAndStartGameplay());
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

            _coroutines.StartCoroutine(LoadAndStartGameplay());
        }

        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesNames.BOOT);
            yield return LoadScene(ScenesNames.GAMEPLAY);

            yield return new WaitForSeconds(1);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(_uiRoot);

            sceneEntryPoint.GoToMainMenuSceneRequested += () =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            };
            
            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadAndStartMainMenu()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesNames.BOOT);
            yield return LoadScene(ScenesNames.MAIN_MENU);

            yield return new WaitForSeconds(1);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            sceneEntryPoint.Run(_uiRoot);

            sceneEntryPoint.GoToGameplaySceneRequested += () =>
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
            };
            
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
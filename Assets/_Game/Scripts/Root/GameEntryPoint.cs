using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Game.Scripts.Game.Root
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void EntryPoint()
        {
            Application.targetFrameRate = 60;

            _instance = new();
            _instance.Run();
        }

        private readonly DIContainer _rootContainer = new();
        private readonly Coroutines _coroutines;
        private readonly UIRootView _uiRoot;

        private GameEntryPoint()
        {
            // Coroutines
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            // Input
            var inputActions = new InputActions();
            inputActions.Gameplay.Enable();

            var inputHandler = new InputHandler();
            inputActions.Gameplay.SetCallbacks(inputHandler);

            _rootContainer.RegisterInstance(inputActions);
            _rootContainer.RegisterInstance(inputHandler);

            // UI
            var prefabUIRoot = Resources.Load<UIRootView>("UI/UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);

            //IConfigProvider configProvider = new LocalConfigProvider();
            //_rootContainer.RegisterInstance(configProvider);

            //IGameStateProvider gameStateProvider = new PlayerPrefsGameStateProvider();
            //_rootContainer.RegisterInstance(gameStateProvider);
        }

        private void Run()
        {
            //await _rootContainer.Resolve<IConfigProvider>().LoadGameConfig();

            _coroutines.StartCoroutine(StartGameplay());
        }

        private IEnumerator StartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            yield return null;

            // GameState может подгружаться из облака, так что необходимо ожидать завершения
            //var isGameStateLoaded = false;
            //_rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            //yield return new WaitUntil(() => isGameStateLoaded);

            var sceneContaiener = new DIContainer(_rootContainer);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(sceneContaiener);

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
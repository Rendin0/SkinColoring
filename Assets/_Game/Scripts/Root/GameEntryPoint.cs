using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using YG;

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
            _coroutines.Init(_rootContainer);
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            GameStateService stateService = new(_coroutines, _rootContainer);
            _rootContainer.RegisterInstance(stateService);

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

            var gameplayExitSceneRequest = new Subject<bool>();
            
            _rootContainer.RegisterInstance(SceneNames.Gameplay, gameplayExitSceneRequest);
            gameplayExitSceneRequest.Subscribe(isCompleted =>
            {
                if (isCompleted)
                    _coroutines.StartCoroutine(StartWinScreen());
                else
                    _coroutines.StartCoroutine(StartGameplay());
            });

            //IConfigProvider configProvider = new LocalConfigProvider();
            //_rootContainer.RegisterInstance(configProvider);
        }

        private void Run()
        {
            //await _rootContainer.Resolve<IConfigProvider>().LoadGameConfig();

            _coroutines.StartCoroutine(StartGameplay());
        }

        private IEnumerator StartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            _rootContainer.Resolve<InputActions>().Gameplay.Enable();

            yield return LoadScene(SceneNames.Boot);

            var stateService = _rootContainer.Resolve<GameStateService>();
            stateService.Load();

            yield return LoadScene(SceneNames.Gameplay);


            int levelId = stateService.GameState.LevelId.Value;

            var sceneContaiener = new DIContainer(_rootContainer);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            // Подписка на событие выхода из сцены
            sceneEntryPoint.Run(sceneContaiener, levelId);
            


            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator StartWinScreen()
        {
            _uiRoot.ShowLoadingScreen();

            var stateService = _rootContainer.Resolve<GameStateService>();
            stateService.GameState.LevelId.Value += 1;
            stateService.Save();

            _rootContainer.Resolve<InputActions>().Gameplay.Disable();

            yield return LoadScene(SceneNames.Boot);
            yield return null;
            yield return LoadScene(SceneNames.WinScreen);

            var sceneContaiener = new DIContainer(_rootContainer);

            var sceneEntryPoint = Object.FindFirstObjectByType<WinScreenEntryPoint>();
            // Подписка на событие выхода из сцены
            sceneEntryPoint.Run(sceneContaiener).Subscribe(_ => _coroutines.StartCoroutine(StartGameplay()));

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
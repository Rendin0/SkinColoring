using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplaySceneUIView _sceneUIRootPrefab;
    [SerializeField] private Camera _skinCamera;

    private DIContainer _sceneContainer;

    public void Run(DIContainer sceneContaiener)
    {
        _sceneContainer = sceneContaiener;
        _sceneContainer.RegisterInstance(_skinCamera);
        GameplayRegistrations.Register(_sceneContainer);

        InitUI(_sceneContainer);
    }

    private void InitUI(DIContainer sceneContainer)
    {
        var uiRoot = sceneContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var uiSceneRootViewModel = sceneContainer.Resolve<GameplaySceneUIViewModel>();
        uiScene.Bind(uiSceneRootViewModel);

        // открытие окон
        var uiManager = sceneContainer.Resolve<GameplayUIManager>();
        uiManager.OpenScreenGameplay();
    }

    private void OnDisable()
    {
        _sceneContainer.Resolve<InputActions>().Gameplay.Disable();
    }
}

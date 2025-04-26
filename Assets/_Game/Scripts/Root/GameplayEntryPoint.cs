using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplaySceneUIView _sceneUIRootPrefab;

    private DIContainer _sceneContainer;

    public void Run(DIContainer sceneContaiener)
    {
        _sceneContainer = sceneContaiener;
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
}

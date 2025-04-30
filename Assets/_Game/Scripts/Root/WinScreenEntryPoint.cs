using R3;
using UnityEngine;

public class WinScreenEntryPoint : MonoBehaviour
{
    [SerializeField] private WinScreenSceneUIView _sceneUIRootPrefab;

    private DIContainer _sceneContainer;
    private readonly Subject<Unit> _exitSceneRequest = new();

    public Subject<Unit> Run(DIContainer sceneContaiener)
    {
        _sceneContainer = sceneContaiener;
        WinScreenRegistrations.Register(_sceneContainer);
        _sceneContainer.RegisterInstance(SceneNames.WinScreen, _exitSceneRequest);



        InitUI(_sceneContainer);

        return _exitSceneRequest;
    }

    private void InitUI(DIContainer sceneContainer)
    {
        var uiRoot = sceneContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var uiSceneRootViewModel = sceneContainer.Resolve<WinScreenSceneUIViewModel>();
        uiScene.Bind(uiSceneRootViewModel);

        // открытие окон
        var uiManager = sceneContainer.Resolve<WinScreenUIManager>();
        uiManager.OpenScreenWin();
    }
}

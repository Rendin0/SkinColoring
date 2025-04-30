using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplaySceneUIView _sceneUIRootPrefab;
    [SerializeField] private Camera _skinCamera;

    [SerializeField] private EditableModel _playerSkin;
    [SerializeField] private EditableModel _originalSkin;

    private DIContainer _sceneContainer;

    private readonly Subject<Unit> _exitSceneRequest = new();

    public Subject<Unit> Run(DIContainer sceneContaiener, string skinName)
    {
        _sceneContainer = sceneContaiener;
        _sceneContainer.RegisterInstance(_skinCamera);
        _sceneContainer.RegisterInstance(SceneNames.Gameplay, _exitSceneRequest);

        GameplayRegistrations.Register(_sceneContainer);

        _playerSkin.SetModelTexture(Resources.Load<Texture2D>($"Skins/skin"));
        _originalSkin.SetModelTexture(Resources.Load<Texture2D>($"Skins/{skinName}"));

        InitUI(_sceneContainer);

        return _exitSceneRequest;
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

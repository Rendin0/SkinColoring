using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplaySceneUIView _sceneUIRootPrefab;
    [SerializeField] private Camera _skinCamera;

    [SerializeField] private EditableModel _playerSkin;
    [SerializeField] private EditableModel _originalSkin;

    [SerializeField] private Skins _skins;

    private DIContainer _sceneContainer;

    private readonly Subject<Unit> _exitSceneRequest = new();


    public Subject<Unit> Run(DIContainer sceneContaiener, int levelId)
    {
        _sceneContainer = sceneContaiener;
        _sceneContainer.RegisterInstance(_skinCamera);
        _sceneContainer.RegisterInstance(SceneNames.Gameplay, _exitSceneRequest);

        GameplayRegistrations.Register(_sceneContainer);

        InitSkins(levelId);
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

    private void InitSkins(int levelId)
    {
        _playerSkin.SetModelTexture(_skins.BlankSkin);

        if (levelId < _skins.SkinTextures.Count)
        {
            _originalSkin.SetModelTexture(_skins.SkinTextures[levelId]);
            return;
        }

        int randomId = Random.Range(0, _skins.SkinTextures.Count);
        _originalSkin.SetModelTexture(_skins.SkinTextures[randomId]);
    }
}

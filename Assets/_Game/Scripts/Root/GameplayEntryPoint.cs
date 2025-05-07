using R3;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplaySceneUIView _sceneUIRootPrefab;
    [SerializeField] private Camera _skinCamera;

    [SerializeField] private EditableModel _playerSkin;
    [SerializeField] private EditableModel _originalSkin;

    [SerializeField] private Skins _skins;

    private DIContainer _sceneContainer;

    public void Run(DIContainer sceneContaiener, int levelId)
    {
        _sceneContainer = sceneContaiener;
        _sceneContainer.RegisterInstance(_skinCamera);

        var (name, colors) = InitSkins(levelId);
        GameplayRegistrations.Register(_sceneContainer, colors, name);

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

    private (string, List<Color>) InitSkins(int levelId)
    {
        _playerSkin.SetModelTexture(_skins.BlankSkin);
        string skinName;

        if (levelId < _skins.SkinTextures.Count)
        {
            _originalSkin.SetModelTexture(_skins.SkinTextures[levelId]);
            skinName = _skins.SkinTextures[levelId].name;
            return (skinName, _originalSkin.GetAllPixels().Distinct().ToList());
        }

        int randomId = Random.Range(0, _skins.SkinTextures.Count);
        skinName = _skins.SkinTextures[randomId].name;
        _originalSkin.SetModelTexture(_skins.SkinTextures[randomId]);

        List<Color> skinColors = _originalSkin.GetAllPixels().Distinct().ToList();

        return (skinName, skinColors);
    }
}

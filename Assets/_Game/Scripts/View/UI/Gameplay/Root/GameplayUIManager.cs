
using System;
using UnityEngine;

public class GameplayUIManager : UIManager
{
    private readonly InputHandler _inputHandler;

    public GameplayUIManager(DIContainer container, InputHandler inputHandler) : base(container)
    {
        this._inputHandler = inputHandler;
    }

    public ScreenGameplayViewModel OpenScreenGameplay()
    {
        var skinCamera = Container.Resolve<Camera>();
        var viewModel = new ScreenGameplayViewModel(this, _inputHandler, skinCamera);

        var sceneUI = Container.Resolve<GameplaySceneUIViewModel>();
        sceneUI.OpenScreen(viewModel);

        return viewModel;
    }

    public ScreenCustomSkinViewModel OpenScreenCustomSkin()
    {
        var skinCamera = Container.Resolve<Camera>();
        var viewModel = new ScreenCustomSkinViewModel(this, _inputHandler, skinCamera);

        var sceneUI = Container.Resolve<GameplaySceneUIViewModel>();
        sceneUI.OpenScreen(viewModel);

        return viewModel;
    }

    public PopupSettingsViewModel OpenPopupSettings()
    {
        var viewModel = new PopupSettingsViewModel();

        var sceneUI = Container.Resolve<GameplaySceneUIViewModel>();
        sceneUI.OpenPopup(viewModel);

        return viewModel;
    }
}
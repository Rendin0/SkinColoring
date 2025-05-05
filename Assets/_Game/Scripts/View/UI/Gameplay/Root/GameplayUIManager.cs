using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class GameplayUIManager : UIManager
{
    private readonly InputHandler _inputHandler;
    private readonly List<Color> _colors;

    public GameplayUIManager(DIContainer container, InputHandler inputHandler) : base(container)
    {
        this._inputHandler = inputHandler;
    }

    public GameplayUIManager(DIContainer container, InputHandler inputHandler, List<Color> colors) : this(container, inputHandler)
    {
        this._colors = colors;
    }

    public ScreenGameplayViewModel OpenScreenGameplay()
    {
        var skinCamera = Container.Resolve<Camera>();
        var gameState = Container.Resolve<GameStateService>().GameState;
        var viewModel = new ScreenGameplayViewModel(this, _inputHandler, skinCamera, gameState, _colors);

        var sceneUI = Container.Resolve<GameplaySceneUIViewModel>();
        sceneUI.OpenScreen(viewModel);

        return viewModel;
    }

    public ScreenCustomSkinViewModel OpenScreenCustomSkin()
    {
        var skinCamera = Container.Resolve<Camera>();
        var gameState = Container.Resolve<GameStateService>().GameState;
        var viewModel = new ScreenCustomSkinViewModel(this, _inputHandler, skinCamera, gameState);

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

    public void SkipLevel()
    {
        var exitSceneRequest = Container.Resolve<Subject<bool>>(SceneNames.Gameplay);
        YGUtils.ShowRewarded(Rewards.SkipLevel);
    }

    public void ContinueLevel()
    {
        var exitSceneRequest = Container.Resolve<Subject<bool>>(SceneNames.Gameplay);

        exitSceneRequest.OnNext(true);
    }
}

using R3;
using System;
using UnityEngine;

public class WinScreenUIManager : UIManager
{
    public WinScreenUIManager(DIContainer container) : base(container)
    {

    }

    public ScreenWinViewModel OpenScreenWin()
    {
        var viewModel = new ScreenWinViewModel(this);

        var sceneUI = Container.Resolve<WinScreenSceneUIViewModel>();
        sceneUI.OpenScreen(viewModel);

        return viewModel;
    }

    public void ExitScene()
    {
        var stateService = Container.Resolve<GameStateService>();
        stateService.GameState.Coins.Value += 50;
        stateService.GameState.Score.Value += 200;

        var exitSceneRequest = Container.Resolve<Subject<Unit>>(SceneNames.WinScreen);
        exitSceneRequest.OnNext(Unit.Default);
    }
}
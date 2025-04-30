
using R3;
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
        var exitSceneRequest = Container.Resolve<Subject<Unit>>(SceneNames.WinScreen);
        exitSceneRequest.OnNext(Unit.Default);
    }
}
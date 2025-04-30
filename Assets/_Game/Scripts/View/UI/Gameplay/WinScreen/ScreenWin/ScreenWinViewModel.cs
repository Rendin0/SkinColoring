using System;

public class ScreenWinViewModel : WindowViewModel
{
    private readonly WinScreenUIManager _uiManager;

    public override string Id => "ScreenWin";

    public ScreenWinViewModel(WinScreenUIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void ContinueLevel()
    {
        _uiManager.ExitScene();
    }
}
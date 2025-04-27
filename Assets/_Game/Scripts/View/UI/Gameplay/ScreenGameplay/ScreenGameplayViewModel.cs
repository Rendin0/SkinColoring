using UnityEngine;

public class ScreenGameplayViewModel : WindowViewModel
{
    private readonly GameplayUIManager _uiManager;

    public override string Id => "ScreenGameplay";

    public ScreenGameplayViewModel(GameplayUIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void OpenSettings()
    {
        _uiManager.OpenPopupSettings();
    }

    public void SetSelectedColor(Color color)
    {

    }
}
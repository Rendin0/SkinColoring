
public class ScreenGameplayViewModel : WindowViewModel
{
    private readonly GameplayUIManager _uiManager;

    public override string Id => "ScreenGameplay";

    public ScreenGameplayViewModel(GameplayUIManager uiManager)
    {
        _uiManager = uiManager;
    }
}
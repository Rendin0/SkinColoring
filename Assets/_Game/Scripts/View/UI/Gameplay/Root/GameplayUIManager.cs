
public class GameplayUIManager : UIManager
{
    public GameplayUIManager(DIContainer container) : base(container)
    {
    }

    public ScreenGameplayViewModel OpenScreenGameplay()
    {
        var viewModel = new ScreenGameplayViewModel(this);

        var sceneUI = Container.Resolve<GameplaySceneUIViewModel>();
        sceneUI.OpenScreen(viewModel);

        return viewModel;
    }
}
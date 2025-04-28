
public static class GameplayRegistrations
{
    public static void Register(DIContainer sceneContainer)
    {
        var inputHandler = sceneContainer.Resolve<InputHandler>();

        sceneContainer.RegisterFactory(_ => new GameplayUIManager(sceneContainer, inputHandler)).AsSingle();
        sceneContainer.RegisterFactory(_ => new GameplaySceneUIViewModel()).AsSingle();
    }

}
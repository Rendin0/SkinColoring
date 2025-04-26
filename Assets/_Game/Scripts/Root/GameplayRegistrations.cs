
public static class GameplayRegistrations
{
    public static void Register(DIContainer sceneContainer)
    {
        sceneContainer.RegisterFactory(_ => new GameplayUIManager(sceneContainer)).AsSingle();
        sceneContainer.RegisterFactory(_ => new GameplaySceneUIViewModel()).AsSingle();
    }

}
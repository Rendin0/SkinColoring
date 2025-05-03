
public static class GameplayRegistrations
{
    public static void Register(DIContainer sceneContainer, System.Collections.Generic.List<UnityEngine.Color> colors)
    {
        var inputHandler = sceneContainer.Resolve<InputHandler>();

        sceneContainer.RegisterFactory(_ => new GameplayUIManager(sceneContainer, inputHandler, colors)).AsSingle();
        sceneContainer.RegisterFactory(_ => new GameplaySceneUIViewModel()).AsSingle();
    }

}

public static class WinScreenRegistrations
{
    public static void Register(DIContainer sceneContainer)
    {
        sceneContainer.RegisterFactory(_ => new WinScreenUIManager(sceneContainer)).AsSingle();
        sceneContainer.RegisterFactory(_ => new WinScreenSceneUIViewModel()).AsSingle();
    }

}
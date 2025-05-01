using UnityEngine;

public class Coroutines : MonoBehaviour 
{
    private DIContainer _rootContainer;

    public void Init(DIContainer rootContainer)
    {
        _rootContainer = rootContainer;
    }

    private void OnApplicationQuit()
    {
        _rootContainer.Resolve<GameStateService>().Dispose();
    }
}
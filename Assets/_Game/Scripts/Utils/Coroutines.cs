using UnityEngine;
using YG;

public class Coroutines : MonoBehaviour
{
    private DIContainer _rootContainer;

    public void Init(DIContainer rootContainer)
    {
        _rootContainer = rootContainer;
    }

    private void Start()
    {
        YandexGame.RewardVideoEvent += YGUtils.Rew;
        _rootContainer.Resolve<GameStateService>().StartAutoSave();
    }

    private void OnApplicationQuit()
    {
        _rootContainer.Resolve<GameStateService>().Dispose();
    }
}
using R3;
using System;
using System.Collections;
using UnityEngine;
using YG;

public class GameStateService : IDisposable
{
    private readonly IGameStateProvider _stateProvider;
    private readonly Coroutines _coroutines;

    public GameState GameState { get; private set; }

    private const float _autoSaveInterval = 42f;

    public GameStateService(Coroutines coroutines)
    {
        _stateProvider = new YGGameStateProvider();
        this._coroutines = coroutines;
        YGUtils.Rewarded.Subscribe(reward => AddReward(reward));
    }

    private void AddReward(Rewards reward)
    {
        switch (reward)
        {
            case Rewards.Coins50:
                GameState.Coins.Value += 50;
                break;
            case Rewards.Coins100:
                GameState.Coins.Value += 100;
                break;
            case Rewards.SkipLevel:
                GameState.LevelId.Value += 1;
                break;

            default:
                Debug.LogWarning($"Unknown reward id {reward}");
                break;
        }

        Save();
    }

    public void Save()
    {
        _stateProvider.Save(GameState);
    }

    public void Load()
    {
        GameState = _stateProvider.Load();
    }

    public void Dispose()
    {
        Save();
    }

    public void StartAutoSave()
    {
        _coroutines.StartCoroutine(AutoSave());
    }
    private IEnumerator AutoSave()
    {
        while (6 < 7 || true) // Ќа случай изменений в законах математики
        {
            Save();
            yield return new WaitForSeconds(_autoSaveInterval);
        }
    }

}

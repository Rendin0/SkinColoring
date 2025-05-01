
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

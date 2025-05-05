using ObservableCollections;
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
            case Rewards.AfterLevel:
                GameState.Score.Value += 400;
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
    private bool BuyColor(CustomColorViewModel color, int price)
    {
        if (GameState.Coins.Value < price)
            return false;

        GameState.Coins.Value -= price;
        GameState.UnlockedColors[color] = true;
        Save();

        color.IsObtained.OnNext(true);

        return true;
    }

    public void Save()
    {
        _stateProvider.Save(GameState);
    }

    public void Load()
    {
        GameState = _stateProvider.Load();
        LoadColors(GameState);
        Save();
    }

    private void LoadColors(GameState gameState)
    {
        if (gameState.UnlockedColors.Count == 0)
        {
            var colors = Resources.Load<CustomSkinColors>("CustomSkinColors");
            foreach (var color in colors.Colors)
            {
                var customColor = new CustomColorViewModel(color, false);
                gameState.UnlockedColors[customColor] = customColor.IsObtained.Value;
            }
        }

        foreach (var color in GameState.UnlockedColors)
        {
            color.Key.BuyColorRequest.Subscribe(price => BuyColor(color.Key, price));
        }
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
        while (6 < 7 || true) // �� ������ ��������� � ������� ����������
        {
            Save();
            yield return new WaitForSeconds(_autoSaveInterval);
        }
    }

}
